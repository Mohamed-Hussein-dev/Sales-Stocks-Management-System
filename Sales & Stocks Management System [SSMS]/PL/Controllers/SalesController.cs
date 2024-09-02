using AutoMapper;
using BLL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PL.Models;
using System.Linq.Expressions;
using System.Transactions;

namespace PL.Controllers
{
    public class SalesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SalesController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            IEnumerable<SaleInvoice> SalesInvoices = _unitOfWork.SalesInvoiceRepository.GetAll();
            var MappedSalesInoices = _mapper.Map<IEnumerable<InvoiceViewModel>>(SalesInvoices);
            return View(MappedSalesInoices);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new InvoiceViewModel()
            {
                Date = DateTime.Now,
                InvoiceItems = new List<InvoiceItem> { new InvoiceItem() }
            };

            PopulateProducts(); // Populate ViewBag with products

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(InvoiceViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CalcTotal();
                    SaleInvoice saleInvoice = _mapper.Map<SaleInvoice>(model);

                    using (TransactionScope SaleTran = new TransactionScope())
                    {
                        foreach (var item in saleInvoice.InvoiceItems)
                        {
                            var product = _unitOfWork.ProductRepository.GetById(item.ProductId);
                            if (item.Quantity > product.StockQuantity)
                            {
                                ModelState.AddModelError("", $"Available from {product.Name}: {product.StockQuantity}");
                                PopulateProducts(); // Ensure products are available in the ViewBag
                                return View(model);
                            }
                            product.StockQuantity -= item.Quantity;
                        }
                        _unitOfWork.SalesInvoiceRepository.Add(saleInvoice);
                        _unitOfWork.Save();
                        SaleTran.Complete();
                    }
                    return RedirectToAction(nameof(Index));
                }

                PopulateProducts(); // Ensure products are available in the ViewBag
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);
                PopulateProducts(); // Ensure products are available in the ViewBag
                return View(model);
            }
        }

        private void PopulateProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAll()
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList();
            ViewBag.Products = products;
        }
    }
}
