using AutoMapper;
using TestAspNetCore_Core.Dtos.Requests;
using TestAspNetCore_Core.Dtos.Responses;
using TestAspNetCore_Core.Entities;

namespace TestAspNetCore.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterCustomerRequestDto, Customer>();
            CreateMap<AddressRequestDto, Address>();
            CreateMap<EmployeeRequestDto, Employee>();

            CreateMap<Customer, CustomerResponseDto>()
                .ForMember(dest => dest.AddressId, options => options.MapFrom(src => src.Address.Id))
                .ForMember(dest => dest.FirstName, options => options.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, options => options.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Company, options => options.MapFrom(src => src.Company))
                .ForMember(dest => dest.City, options => options.MapFrom(src => src.City))
                .ForMember(dest => dest.State, options => options.MapFrom(src => src.State))
                .ForMember(dest => dest.Country, options => options.MapFrom(src => src.Country))
                .ForMember(dest => dest.PostalCode, options => options.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Phone, options => options.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Fax, options => options.MapFrom(src => src.Fax))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.Tag, options => options.MapFrom(src => src.Tag))
                .ForMember(dest => dest.AddressId, options => options.MapFrom(src => src.Address.Id))
                .ForMember(dest => dest.SupportRepId, options => options.MapFrom(src => src.SupportRep.Id));
        }
    }
}
