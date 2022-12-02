using AutoMapper;
using GSC_FinalProject.Entities;
using GSC_FinalProject.Models;

namespace GSC_FinalProject.Dto
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Loan, LoanDTO>().ReverseMap();
            CreateMap<Thing, ThingViewModel>().ReverseMap();
        }
    }
}
