﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using Alborz.ServiceLayer.IService;
using PagedList;

namespace AlborzMarket.Controllers
{
    public class ProductController : Controller
    {
        readonly IPriceService _price;
        readonly IProductService _product;
        readonly ICategoryService _category;
        readonly IPropertyService _property;
        readonly IUnitOfWork _uow;
        private ProductDTO common;
        private List<ProductDTO> commonList;

        public ProductController(IUnitOfWork uow, IProductService product, ICategoryService category, IPropertyService property, IPriceService price)
        {
            _uow = uow;
            _product = product;
            _category = category;
            _product = product;
            _price = price;
        }
        [HttpGet]
        public async Task<ActionResult> Index(ProductDTO model)
        {
            commonList = new List<ProductDTO>();
            ViewBag.CurrentSort = model.SortOrder;
            ViewBag.Category = model.SortOrder == "category" ? "category_desc" : "category";
            ViewBag.Title = model.SortOrder == "title" ? "title_desc" : "title";
            ViewBag.Code = model.SortOrder == "code" ? "code_desc" : "code";
            ViewBag.Brand = model.SortOrder == "brand" ? "brand_desc" : "brand";
            ViewBag.IsBuyable = model.SortOrder == "isBuyable" ? "isBuyable_desc" : "isBuyable";
            ViewBag.Quantity = model.SortOrder == "quantity" ? "quantity_desc" : "quantity";
            if (model.SearchString != null)
            {
                model.Page = 1;
            }
            else
            {
                model.SearchString = model.CurrentFilter;
            }
            var product = await _product.GetAllProductsAsync();
            foreach (var item in product)
            {
                var price = _price.GetLastPriceProduct(item.Id).Price;

                item.Price =  price!= null ? price :0 ;
            }
            //if (!String.IsNullOrEmpty(model.SearchString))
            //{
            //    product = await _product.GetProductsBySearchItemAsync(model.SearchString);
            //}
            //else
            //{
            //    product = await _product.GetAllProductsAsync();
            //}
            switch (model.SortOrder)
            {
                case "title_desc":
                    product = product.OrderByDescending(s => s.Title).ToList();
                    break;
                case "code":
                    product = product.OrderBy(s => s.Code).ToList();
                    break;
                case "code_desc":
                    product = product.OrderByDescending(s => s.Code).ToList();
                    break;
                case "brand":
                    product = product.OrderBy(s => s.Brand).ToList();
                    break;
                case "brand_desc":
                    product = product.OrderByDescending(s => s.Brand).ToList();
                    break;
                case "isBuyable":
                    product = product.OrderBy(s => s.IsBuyable).ToList();
                    break;
                case "isBuyable_desc":
                    product = product.OrderByDescending(s => s.IsBuyable).ToList();
                    break;
                case "quantity":
                    product = product.OrderBy(s => s.Quantity).ToList();
                    break;
                case "quantity_desc":
                    product = product.OrderByDescending(s => s.Quantity).ToList();
                    break;
                default:
                    product = product.OrderBy(s => s.Title).ToList();
                    break;
            }
            int pageSize = 10;
            int pageNumber = (model.Page ?? 1);
            model.Categories = await _category.GetAllCategoriesAsync();
            model.ProductsPageList = product.ToPagedList(pageNumber, pageSize);
            return View(model);
        }
        public async Task<ActionResult> ProductsByTitle(string searchItem)
        {
            commonList = new List<ProductDTO>();
            common = new ProductDTO();
            if (string.IsNullOrEmpty(searchItem))
            {
                return View("index","home");
            }
            var product = _product.GetProductsBySearchItem(searchItem);
            var prices = _price.GetAllPrices();
            if (prices.Count > 0)
            {
                foreach (var item in product)
                {
                    item.Price = prices.Where(x => x.ProductId == item.Id).Any() ? prices.Where(x => x.ProductId == item.Id).OrderByDescending(x => x.Id).FirstOrDefault().Price : 0;
                }
            }
            //if (common.SearchString != null)
            //{
            //    common.Page = 1;
            //}
            //else
            //{
            //    common.SearchString = common.CurrentFilter;
            //}
            if (!String.IsNullOrEmpty(searchItem))
            {
                product = _product.GetProductsBySearchItem(searchItem);
            }
            else
            {
                product = await _product.GetAllProductsAsync();
            }
            int pageSize = 10;
            int pageNumber = (common.Page ?? 1);
            common.Categories = await _category.GetAllCategoriesAsync();
            common.ProductsPageList = product.ToPagedList(pageNumber, pageSize);
            return View(common);
            //return View(product);
        }
        
        public async Task<ActionResult> ProductsByCategory(ProductDTO model)
        {
            commonList = new List<ProductDTO>();
            common = new ProductDTO();
            if (model.CategoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = new List<ProductDTO>();
            var prices = _price.GetAllPrices();
   
            //if (common.SearchString != null)
            //{
            //    common.Page = 1;
            //}
            //else
            //{
            //    common.SearchString = common.CurrentFilter;
            //}
            if (!String.IsNullOrEmpty(model.SearchString))
            {
                product =  _product.GetProductsBySearchItem(model.SearchString);
            }
            else
            {
                product =await  _product.GetProductsByCategoryIdAsync(model.CategoryId);
            }
            if (prices.Count > 0)
            {
                foreach (var item in product)
                {
                    item.Price = prices.Where(x => x.ProductId == item.Id).Any() ? prices.Where(x => x.ProductId == item.Id).OrderByDescending(x => x.Id).FirstOrDefault().Price : 0;
                }
            }
            int pageSize = 10;
            int pageNumber = (common.Page ?? 1);
            common.Categories = await _category.GetAllCategoriesAsync();
            common.ProductsPageList = product.ToPagedList(pageNumber, pageSize);
            common.CategoryId = model.CategoryId;
            return View(common);
            //return View(product);
        }

        public async Task<ActionResult> Details(int? id, CancellationToken ct = default(CancellationToken))
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = await _product.GetProductAsync(id);
            var properties = await _property.GetAllPropertiesByProductIdAsync(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        ////[Authorize(Roles = "admin , SuperViser")]
        public async Task<ActionResult> Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    common = new ProductDTO()
                    {
                        Categories = await _category.GetAllCategoriesAsync()
                    };
                    return View(common);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductDTO model)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        model.Categories = await _category.GetAllCategoriesAsync();
                        model.StartDate = DateTime.Now;
                        model.EndDate = DateTime.Now;
                        if (ModelState.IsValid)
                        {
                            var product = await _product.AddNewProductAsync(model);
                            var price = new PriceDTO
                            {
                                ProductId = product.Id,
                                Price = model.Price
                            };
                            await _price.AddNewPriceAsync(price);
                            _uow.SaveAllChanges();
                            return RedirectToAction("create", "ProductDetail", new { id = product.Id });
                        }
                        return View(model);
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public async Task<ActionResult> Edit(int? id, string returnController, string returnAction)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var product = await _product.GetProductAsync(id);
                    if (product == null)
                    {
                        return HttpNotFound();
                    }
                    //ViewBag.ReturnController = returnController;
                    //ViewBag.ReturnAction = returnAction;
                    product.Categories = await _category.GetAllCategoriesAsync();
                    product.CategoryName = product.Categories.Where(x => x.Id == product.CategoryId).FirstOrDefault().Title;
                    product.Price = _price.GetLastPriceProduct(id).Price;
                    product.ProductId =(int)id;
                    return View(product);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductDTO product, string returnController, string returnAction)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        product.Categories = await _category.GetAllCategoriesAsync();
                        await _product.UpdateProductAsync(product);
                        var price = new PriceDTO
                        {
                            ProductId = product.Id,
                            Price = product.Price
                        };
                        var existPrice = _price.GetLastPriceProduct(product.Id);
                        if (existPrice.Price != product.Price)
                        {
                           await _price.DeleteAsync(existPrice.Id);
                         await _price.AddNewPriceAsync(price);
                        } 
                    }
                    if (!string.IsNullOrEmpty(returnController) && !string.IsNullOrEmpty(returnAction))
                    {
                        return RedirectToAction(returnAction, returnController, new { id = product.Id });
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception)
            {
                return View("Index");
            }
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        var product = await _product.GetProductAsync(id);
                        if (product == null)
                        {
                            return HttpNotFound();
                        }
                        return View(product);
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception)
            {
                return View();
            }
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        await _product.DeleteAsync(id);
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
