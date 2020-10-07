using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using Alborz.ServiceLayer.IService;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AlborzMarket.Controllers
{
    public class HomeController : Controller
    {
        readonly IProductService _product;
        readonly IPriceService _price;
        readonly IUnitOfWork _uow;
        private HomeDTO common;
        private List<HomeDTO> commonList;

        public HomeController(IUnitOfWork uow, IProductService product, IPriceService price)
        {
            _price = price;
            _product = product;
            _uow = uow;
        }
        [HttpGet]
        public ActionResult Index()
        {
            common = new HomeDTO();
            common.Newest = _product.GetAllRecentProducts();
            var prices = _price.GetAllPrices();
            if (prices.Count > 0)
            {
                foreach (var item in common.Newest)
                {
                    item.Price = prices.Where(x => x.ProductId == item.Id).Any() ? prices.Where(x => x.ProductId == item.Id).OrderByDescending(x => x.Id).FirstOrDefault().Price : 0;
                }
            }

            common.MostSale = _product.GetAllRecentProducts();
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
