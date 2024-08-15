using AutoMapper;
using DAL.Models;
using PL.Models;

namespace PL.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() { 
            CreateMap<Product , ProductViewModel>();
            CreateMap<ProductViewModel, Product>().ForMember(dist => dist.InvoiceItems , opt => opt.Ignore());
        }
    }
}
