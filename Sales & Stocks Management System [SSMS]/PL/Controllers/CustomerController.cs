using AutoMapper;
using BLL.Interfaces;
using BLL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PL.Models;
using System.Linq;

namespace PL.Controllers
{
    public class CustomerController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(UnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            IEnumerable<Customer> customers = _unitOfWork.CustomerRepository.GetAll();
            IEnumerable<CustomerViewModel> MappedCustomers = _mapper.Map<IEnumerable<CustomerViewModel>>(customers);
            return View(MappedCustomers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(model);

                _unitOfWork.CustomerRepository.Add(customer);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            return Details(id , "Edit");
        }

        [HttpPost]
        public IActionResult Edit(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var mappedCustomer = _mapper.Map<Customer>(model);

                _unitOfWork.CustomerRepository.Update(mappedCustomer);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Details(int id , string ViewName = "Details")
        {
            var customer = _unitOfWork.CustomerRepository.Entities.Include(C => C.Invoices).ThenInclude(inv => inv.InvoiceItems).Where(C => C.Id == id).FirstOrDefault();
            if(customer == null)
                   return NotFound();
            var mappedCustomer = _mapper.Map<CustomerViewModel>(customer);
            return View(ViewName , mappedCustomer);
        }

        public IActionResult Delete(int id)
        {
            return Details(id,"Delete");
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirme([FromRoute]int id , CustomerViewModel model)
        {
            if (model.Id != id) return BadRequest();
            var customer = _unitOfWork.CustomerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _unitOfWork.CustomerRepository.Delete(customer);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
