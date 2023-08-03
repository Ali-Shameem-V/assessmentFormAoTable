using assessmentFormAoTable.Dto;
using assessmentFormAoTable.Models;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace assessmentFormAoTable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly DemoContextApi demoContext;
        private readonly IMapper _mapper;

        public AssessmentController(DemoContextApi demoContext, IMapper mapper)
        {
            this.demoContext = demoContext;
            this._mapper = mapper;
        }

        //add record to Form for any Table_name available in AOTable

        [HttpPost("Add-Form-by-TableName/{name}")]
        public async Task<ActionResult<List<Form>>> add(String name, Form form)
        {
            try
            {
                var table = await demoContext.Aotables.FirstOrDefaultAsync(x => x.Name == name);
                if (table == null)
                {
                    return NotFound("Not table with the Name " + name);
                }
                form.Id = Guid.NewGuid();
                form.TableId = table.Id;
                await demoContext.Forms.AddAsync(form);
                await demoContext.SaveChangesAsync();
                return Ok(form);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        //Update a record in Form by passing Form_Name

        [HttpPut("Update-by-formName/{name}")]
        public async Task<IActionResult> updateForm(String name, Formdto form)
        {
            try
            {
                var formname = await demoContext.Forms.FirstOrDefaultAsync(_ => _.Name == name);
                if (formname == null)
                {
                    return BadRequest("Form Name Is Invalid");
                }

                form.Id = formname.Id;
                var update = _mapper.Map<Form>(form);
                _mapper.Map(update, formname);
                demoContext.Entry(formname).CurrentValues.SetValues(update);
                await demoContext.SaveChangesAsync();
                return Ok(update);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        //Delete a record in Form by passing Id as parameter 

        [HttpDelete("Delete-by-Id/{id}")]
        public async Task<ActionResult<List<Form>>> delete(Guid id)
        {
            try
            {
                var delete = demoContext.Forms.FirstOrDefault(_ => _.Id == id);
                if (id == null || delete == null)
                {
                    return BadRequest("Invalid Form Id");
                }
                demoContext.Forms.Remove(delete);
                await demoContext.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return StatusCode(500);

            }

        }

        // Get Form Record By Passing Id as parameter

        [HttpGet("Get-by-FormId/{id}")]
        public async Task<ActionResult<List<Form>>> GetbyId(Guid id)
        {
            try
            {
                var find = await demoContext.Forms.FindAsync(id);
                if (find != null)
                {
                    var form = await demoContext.Forms.Where(_ => _.Id == id).ToListAsync();
                    return Ok(form);
                }
                return BadRequest("No Form Records for the Id "+id);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
            

        }
        
        // Get All Form By passing Form_type As parameter
        
        [HttpGet("Get-all-by-type/{type}")]
        public async Task<ActionResult<List<Form>>> getallrec(String type)
        {
            try
            {
                var result = await demoContext.Forms.Where(_ => _.Type == type).ToListAsync();
                if (result.Any())
                {
                    return result;

                }
                return BadRequest("Invalid Form Type");
            }
            catch 
            {
               return StatusCode(500);
            }

            
        }
        
        //Get all Form and Aotable Name by passing Table_Id as parameter
        
        [HttpGet("Get-all-by-TableId/{tableId:Guid}")]
        public async Task<ActionResult<List<Aotable>>> GetAllByTableId(Guid tableId)
        {
            try 
            {
                var res = await demoContext.Aotables.FirstOrDefaultAsync(_ => _.Id == tableId);
                var result = await demoContext.Forms.Include("Table").Where(_ => _.TableId == tableId).ToListAsync();
                if (res != null && result.Any())
                {
                    var re = new
                    {
                        result,
                        tablename = res.Name
                    };
                    return Ok(re);
                }
                return BadRequest("No Table Id found for Form Table");
            }
            catch 
            { 
                return StatusCode(500); 
            }
            
        }
        
        //Get all Form records by passing Table_name as parameter
        
        [HttpGet("Get-all-by-TableName/{name}")]
        public async Task<ActionResult<List<Form>>> getall(String name)
        {
            try
            {
                var result = await demoContext.Forms.Where(_ => _.Table.Name == name).ToListAsync();
                if (result.Any())
                {
                    return Ok(result);
                }
                return BadRequest("No Form records associated with Table Name" + name);
            }
            catch
            {
                return StatusCode(400);
            }
            
        }
                      
    }
}
