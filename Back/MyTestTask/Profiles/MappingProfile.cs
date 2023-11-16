using AutoMapper;
using MyTestTask.Models;

namespace MyTestTask.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactDTO>();
            CreateMap<ContactDTO, Contact>();
        }
    }
}