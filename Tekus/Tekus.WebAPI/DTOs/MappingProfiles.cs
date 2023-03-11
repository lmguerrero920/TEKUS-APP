using AutoMapper;
using Tekus.Core.Entities;

namespace Tekus.WebAPI.DTOs
{
    public class MappingProfiles:Profile
    {
     public MappingProfiles() 
      { 
      CreateMap<Supplier, SupplierDto>().ForMember(p=>p.Services,x=> x.MapFrom(a=> a.Services));
       
      CreateMap<Services, ServicesDto>().ForMember(p => p.CountryName, x => x.MapFrom(a => a.Country.Name));
            CreateMap<User, UserDto>().ReverseMap();

        }

    }
}
