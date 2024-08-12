using AutoMapper;
using DAL.Models;
using PL.Models;

namespace PL.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() { 
            CreateMap<ProductViewModel , Product>().ReverseMap();
        }
    }
}
