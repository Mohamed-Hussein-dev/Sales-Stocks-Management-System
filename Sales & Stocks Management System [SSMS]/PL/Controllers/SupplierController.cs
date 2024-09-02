using AutoMapper;
using BLL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PL.Models;

namespace PL.Controllers
{
    public class SupplierController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SupplierController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            IEnumerable<Supplier> Suppliers = _unitOfWork.SupplierRepository.GetAll();
            IEnumerable<SupplierViewModel> MappedSuppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(Suppliers);
            return View(MappedSuppliers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Supplier = _mapper.Map<Supplier>(model);

                _unitOfWork.SupplierRepository.Add(Supplier);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit(SupplierViewModel model)
        {
            if (ModelState.IsValid)
            {

                var mappedSupplier = _mapper.Map<Supplier>(model);

                _unitOfWork.SupplierRepository.Update(mappedSupplier);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Details(int id, string ViewName = "Details")
        {
            var Supplier = _unitOfWork.SupplierRepository.Entities.Include(C => C.Invoices).Where(C => C.Id == id).FirstOrDefault();
            if (Supplier == null)
                return NotFound();
            var mappedSupplier = _mapper.Map<SupplierViewModel>(Supplier);
            return View(ViewName, mappedSupplier);
        }

        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirme([FromRoute] int id, SupplierViewModel model)
        {
            if (model.Id != id) return BadRequest();
            var Supplier = _unitOfWork.SupplierRepository.GetById(id);
            if (Supplier == null)
            {
                return NotFound();
            }

            _unitOfWork.SupplierRepository.Delete(Supplier);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
