using AutoMapper;
using BLL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PL.Models;

namespace PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: ProductController
        public ActionResult Index(string name, string category, int? minStock, int? maxStock)
        {
            IEnumerable<Product> products = _unitOfWork.ProductRepository.GetAll();

            // Apply filters
            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.Contains(category));
            }

            if (minStock.HasValue)
            {
                products = products.Where(p => p.StockQuantity >= minStock.Value);
            }

            if (maxStock.HasValue)
            {
                products = products.Where(p => p.StockQuantity <= maxStock.Value);
            }

            // Map products to ProductViewModel
            IEnumerable<ProductViewModel> MappedProducts = _mapper.Map<IEnumerable<ProductViewModel>>(products);

            return View(MappedProducts);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            var productViewModel = _mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = _mapper.Map<Product>(productViewModel);
                    _unitOfWork.ProductRepository.Add(product);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                return View(productViewModel);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            var productViewModel = _mapper.Map<ProductViewModel>(product);
            productViewModel.LastPrice = product.ProductPrices.LastOrDefault()?.Price??0;
            return View(productViewModel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute] int id, ProductViewModel productViewModel)
        {
            try
            {
                if(id != productViewModel.Id)
                    return BadRequest();

                if (ModelState.IsValid)
                {
                    var product = _unitOfWork.ProductRepository.GetById(id);
                    if (product == null)
                    {
                        return NotFound();
                    }

                    product.Name = productViewModel.Name;
                    product.Description = productViewModel.Description;
                    product.StockQuantity = productViewModel.StockQuantity;
                    product.Category = productViewModel.Category;
                    var lastPrice = product.ProductPrices.LastOrDefault();
                    if (lastPrice != null)
                    {
                        lastPrice.Price = productViewModel.LastPrice;
                    }
                    else
                    {
                        product.ProductPrices.Add(new ProductPrice()
                        {
                            Price = productViewModel.LastPrice,
                            Product = product,
                            DateTime = DateTime.Now
                        });
                    }
                    _unitOfWork.ProductRepository.Update(product);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                return View(productViewModel);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete([FromRoute]int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            
            var productViewModel = _mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([FromRoute]int id, ProductViewModel collection)
        {
            try
            {
                if(collection.Id != id) return BadRequest();

                var product = _unitOfWork.ProductRepository.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }

                _unitOfWork.ProductRepository.Delete(product);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
