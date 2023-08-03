using assessmentFormAoTable.Dto;
using assessmentFormAoTable.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
