using assessmentFormAoTable.Dto;
using assessmentFormAoTable.Models;
using AutoMapper;

namespace assessmentFormAoTable
{
    public class AddMapperProfile : Profile
    {
        public AddMapperProfile()
        {
            CreateMap<Formdto, Form>();
        }
    }
}
