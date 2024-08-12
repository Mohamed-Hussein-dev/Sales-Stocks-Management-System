using AutoMapper;
using BLL.Repositories;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System.Diagnostics;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger , UnitOfWork unitOfWork , IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var OutOfStockProducts = _unitOfWork.ProductRepository.Entity.Where(P => P.StockQuantity <= 2).ToList();
            var mapped = _mapper.Map<IEnumerable<ProductViewModel>>(OutOfStockProducts);
            DashboardViewModel model = new DashboardViewModel()
            {
                LowStockProducts = mapped,
            };
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
