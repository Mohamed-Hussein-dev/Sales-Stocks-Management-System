using AutoMapper;
using DAL.Models;
using PL.Models;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
       
        CreateMap<Supplier, SupplierViewModel>();
        CreateMap<SupplierViewModel, Supplier>().ForMember(dest => dest.Invoices, opt => opt.Ignore());
    }
}
