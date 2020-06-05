using System;
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

        readonly IProductService _product;
        readonly ICategoryService _category;
        readonly IPropertyService _property;
        readonly IUnitOfWork _uow;
        private ProductDTO common;
        private List<ProductDTO> commonList;

        public ProductController(IUnitOfWork uow, IProductService product, ICategoryService category, IPropertyService property)
        {
            _uow = uow;
            _product = product;
            _category = category;
            _product = product;
        }
        [HttpGet]
        public async Task<ActionResult> Index(ProductDTO model)
        {
            commonList = new List<ProductDTO>();
            ViewBag.CurrentSort = model.sortOrder; 
            ViewBag.Category = model.sortOrder == "category" ? "category_desc" : "category";
            ViewBag.Title = model.sortOrder == "title" ? "title_desc" : "title";
            ViewBag.Code = model.sortOrder == "code" ? "code_desc" : "code";
            ViewBag.Brand = model.sortOrder == "brand" ? "brand_desc" : "brand";
            ViewBag.IsBuyable = model.sortOrder == "isBuyable" ? "isBuyable_desc" : "isBuyable";
            ViewBag.Quantity = model.sortOrder == "quantity" ? "quantity_desc" : "quantity";
            if (model.searchString != null)
            {
                model.page = 1;
            }
            else
            {
                model.searchString = model.currentFilter;
            }
            var product = new List<ProductDTO>();
            if (!String.IsNullOrEmpty(model.searchString))
            {
                product = await _product.GetProductsBySearchItemAsync(model.searchString);
            }
            else
            {
                product = await _product.GetAllProductsAsync();
            }
            switch (model.sortOrder)
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
            int pageNumber = (model.page ?? 1); 
            model.Categories = await _category.GetAllCategoriesAsync();
            model.ProductsPageList= product.ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        //public async Task<ActionResult> Details(int? id, CancellationToken ct = default(CancellationToken))
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var entity = await _product.GetProductAsync(id); 
        //    if (entity == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(entity);
        //}

        ////[Authorize(Roles = "admin , SuperViser")]
        //public async Task<ActionResult> Create()
        //{
        //    //if (User.Identity.IsAuthenticated)
        //    //{
        //    //    if (User.IsInRole("Admin"))
        //    //    {
        //    common = new ProductDTO()
        //    {
        //        Categories = await _category.GetAllCategoriesAsync()
        //    };
        //    return View(common);
        //    //    }
        //    //}
        //    //return RedirectToAction("login", "Account");
        //}

        ////[Authorize(Roles = "admin , SuperViser")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(ProductDTO rout)
        //{
        //    try
        //    {
        //        if (User.Identity.IsAuthenticated)
        //        {
        //            if (User.IsInRole("Admin"))
        //            {
        //                rout.Categories = await _category.GetAllCategoriesAsync();
        //                if (ModelState.IsValid)
        //                {
        //                    await _product.AddNewProductAsync
        //                        (rout);
        //                    _uow.SaveAllChanges();
        //                    return RedirectToAction("Index");
        //                }
        //                return View();
        //            }
        //        }
        //        return RedirectToAction("login", "Account");
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        ////[Authorize(Roles = "admin , SuperViser")]
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    //if (User.Identity.IsAuthenticated)
        //    //{
        //    //    if (User.IsInRole("Admin"))
        //    //    {

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var category = await _category.GetCategoryAsync(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    category.Categories = await _category.GetAllCategoriesAsync();
        //    return View(category);
        //    //        }
        //    //    }
        //    //    return RedirectToAction("login", "Account");
        //}

        ////[Authorize(Roles = "admin , SuperViser")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(ProductDTO category)
        //{
        //    try
        //    {
        //        //if (User.Identity.IsAuthenticated)
        //        //{
        //        //    if (User.IsInRole("Admin"))
        //        //    {

        //        category.Categories = await _product.GetAllCategoriesAsync();

        //        if (ModelState.IsValid)
        //        {
        //            await _product.UpdateCategoryAsync(category);
        //        }
        //        return RedirectToAction("Index");
        //        // }
        //        //    }
        //        //    return RedirectToAction("login", "Account");
        //    }
        //    catch (Exception)
        //    {
        //        return View("Index");
        //    }
        //}

        ////[Authorize(Roles = "admin , SuperViser")]
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    try
        //    {
        //        if (User.Identity.IsAuthenticated)
        //        {
        //            if (User.IsInRole("Admin"))
        //            {
        //                if (id == null)
        //                {
        //                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //                }
        //                var category = await _product.GetCategoryAsync(id);
        //                category.Categories = await _product.GetAllCategoriesAsync();
        //                if (category == null)
        //                {
        //                    return HttpNotFound();
        //                }
        //                return View(category);
        //            }
        //        }
        //        return RedirectToAction("login", "Account");
        //    }
        //    catch (Exception)
        //    {
        //        return View();
        //    }
        //}

        ////[Authorize(Roles = "admin , SuperViser")]
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        if (User.Identity.IsAuthenticated)
        //        {
        //            if (User.IsInRole("Admin"))
        //            {
        //                await _product.DeleteAsync(id);
        //                return RedirectToAction("Index");
        //            }
        //        }
        //        return RedirectToAction("login", "Account");
        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

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
