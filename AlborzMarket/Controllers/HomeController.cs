using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using Alborz.ServiceLayer.IService;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AlborzMarket.Controllers
{
    public class HomeController : Controller
    {
        readonly IProductService _product;
        readonly IUnitOfWork _uow;
        private HomeDTO common;
        private List<HomeDTO> commonList;

        public HomeController(IUnitOfWork uow, IProductService product)
        {
            _product = product;
            _uow = uow;
        }
        [HttpGet]
        public ActionResult Index()
        {
            common = new HomeDTO();
            //common.Newest = _product.GetAllProducts();
            //common.MostSale = _product.GetAllProducts();
            common.Newest = new List<ProductDTO>();
            common.Newest.Add(_product.GetProduct(10));
            common.Newest.Add(_product.GetProduct(11));
            common.Newest.Add(_product.GetProduct(46));
            common.Newest.Add(_product.GetProduct(50));
            return View(common);
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
