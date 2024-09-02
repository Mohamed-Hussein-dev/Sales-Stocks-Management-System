using AutoMapper;
using BLL.Interfaces.Invoice;
using DAL.Models;
using PL.Models;


namespace PL.MappingProfiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile() { 
        
            CreateMap<SaleInvoice, InvoiceViewModel>().ReverseMap();
            CreateMap<PurchaseInvoice, InvoiceViewModel>().ReverseMap(); 
        }
    }
}
