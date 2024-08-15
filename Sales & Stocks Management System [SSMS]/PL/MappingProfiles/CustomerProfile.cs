using AutoMapper;
using DAL.Models;
using PL.Models;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
       
        CreateMap<Customer, CustomerViewModel>();
        CreateMap<CustomerViewModel, Customer>().ForMember(dest => dest.Invoices, opt => opt.Ignore());
    }
}
