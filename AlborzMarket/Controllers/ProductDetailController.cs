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
    public class ProductDetailController : Controller
    {
        readonly IProductDetailService _productDetail;
        readonly IProductService _product;
        readonly ICategoryService _category;
        readonly IColorService _color;
        readonly IUnitOfWork _uow;
        private ProductDetailDTO common;
        private List<ProductDetailDTO> commonList;

        public ProductDetailController(IUnitOfWork uow, IProductDetailService productDetail, ICategoryService category, IProductService product, IColorService color)
        {
            _uow = uow;
            _product = product;
            _category = category;
            _productDetail = productDetail;
            _color = color;
        }
        [HttpGet]
        public async Task<ActionResult> ProductDetailListByProductId(ProductDetailDTO model)
        {
            var productDetail = await _productDetail.GetAllProductDetailByProductIdAsync((int)model.ProductId);
            ViewBag.CurrentSort = model.SortOrder;
            ViewBag.Quantity = model.SortOrder == "quantity" ? "quantity_desc" : "quantity";

            switch (model.SortOrder)
            {
                case "quantity":
                    productDetail = productDetail.OrderBy(s => s.Quantity).ToList();
                    break;
                case "quantity_desc":
                    productDetail = productDetail.OrderByDescending(s => s.Quantity).ToList();
                    break;
                default:
                    productDetail = productDetail.OrderBy(s => s.Id).ToList();
                    break;
            }
            int pageSize = 30;
            int pageNumber = (model.Page ?? 1);
            model.ProductDetailsPageList = productDetail.ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        ////[Authorize(Roles = "admin , SuperViser")]
        [HttpGet]
        public async Task<ActionResult> Create(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    common = new ProductDetailDTO()
                    {
                        Colors = await _color.GetAllColorsAsync(),
                        ProductDetails = new List<ProductDetailDTO>(),
                        ProductId = id
                    };
                    return View(common);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductDetailDTO model)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        if (ModelState.IsValid)
                        {
                            if (model.ProductId == null)
                            {
                                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                            }
                            var colors = await _color.GetAllColorsAsync();
                            if (model.ProductDetails != null)
                            {
                                foreach (var item in model.ProductDetails)
                                {
                                    if (item.ColorId != null)
                                    {
                                        item.ProductId = model.ProductId;
                                        item.Colors = colors;
                                        await _productDetail.AddNewProductDetailAsync(item);
                                    }
                                }
                                //await _productDetail.AddAllProductDetailsAsync(model.ProductDetails);
                                _uow.SaveAllChanges();

                            }
                            return RedirectToAction("CreatePropertyForProduct", "Property", new { id = model.ProductId });
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
                    var productDetail = await _productDetail.GetProductDetailAsync(id);
                    if (productDetail == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.ReturnController = returnController;
                    ViewBag.ReturnAction = returnAction;
                    return View(productDetail);
                }
            }
            return RedirectToAction("login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductDetailDTO productDetail, string returnController, string returnAction)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {

                        if (ModelState.IsValid)
                        {
                            await _productDetail.UpdateProductDetailAsync(productDetail);
                            if (!string.IsNullOrEmpty(returnController) && !string.IsNullOrEmpty(returnAction))
                            {
                                return RedirectToAction(returnAction, returnController, new { id = productDetail.ProductId });
                            }
                        }
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

        public async Task<ActionResult> EditAll(int? id, string returnController, string returnAction)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {

                    if (id == 0)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    var productDetail = await _productDetail.GetAllProductDetailByProductIdAsync((int)id);
                    var colors = await _color.GetAllColorsAsync();
                    common = new ProductDetailDTO();
                    foreach (var item in productDetail)
                    {
                        item.Colors = colors;
                        item.ColorName = colors.Where(x => x.Id == item.ColorId).FirstOrDefault().Title;
                        item.ProductId = id;
                        item.Id = (int)id;
                    }
                    if (productDetail == null)
                    {
                        return HttpNotFound();
                    }
                    common.ProductDetails = productDetail;
                    common.ProductId = id;
                    return View(common);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAll(ProductDetailDTO productDetail, string returnController, string returnAction)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        if (productDetail.ProductDetails != null)
                        {
                            foreach (var item in productDetail.ProductDetails)
                            {
                                item.ProductId = productDetail.ProductId;
                            }
                        }
                            if (productDetail != null)
                            {
                                var exist = await _productDetail.GetAllProductDetailByProductIdAsync((int)productDetail.ProductId);
                                if (exist.Any())
                                {
                                    foreach (var item in exist)
                                    {
                                        await _productDetail.DeleteByProductIdAsync((int)item.ProductId);
                                    }
                                }
                                if (productDetail.ProductDetails != null)
                                {
                                    foreach (var item in productDetail.ProductDetails)
                                    {
                                        item.ProductId = (int)productDetail.ProductId;
                                        //item.Id = (int)id;
                                    }
                                    await _productDetail.AddAllProductDetailsAsync(productDetail.ProductDetails);
                                }
                            }
                       
                        _uow.SaveAllChanges();
                        return RedirectToAction("Index", "product");
                    }
                }
                return RedirectToAction("login", "Account");
            }
            catch (Exception e)
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
                        var productDetail = await _productDetail.GetProductDetailAsync(id);
                        if (productDetail == null)
                        {
                            return HttpNotFound();
                        }
                        return View(productDetail);
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
                        await _productDetail.DeleteAsync(id);
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

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteByProductIdConfirmed(int productId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        await _productDetail.DeleteByProductIdAsync(productId);
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

        public async Task<ActionResult> ProductDetailsEntry()
        {
            common = new ProductDetailDTO()
            {
                Colors = await _color.GetAllColorsAsync(),
                ProductDetails = new List<ProductDetailDTO>(),
            };

            return PartialView("ProductDetailsEntry", common);
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