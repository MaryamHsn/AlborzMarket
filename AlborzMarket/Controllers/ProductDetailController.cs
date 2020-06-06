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
    public class ProductDetailController : Controller
    {
        readonly IProductDetailService _productDetail;
        readonly IProductService _product;
        readonly ICategoryService _category; 
        readonly IUnitOfWork _uow;
        private ProductDetailDTO common;
        private List<ProductDetailDTO> commonList;

        public ProductDetailController(IUnitOfWork uow, IProductDetailService productDetail, ICategoryService category, IProductService product)
        {
            _uow = uow;
            _product = product;
            _category = category;
            _productDetail = productDetail;
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
        public  ActionResult Create()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    if (User.IsInRole("Admin"))
            //    {

            return View();
            //    }
            //}
            //return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductDetailDTO model)
        {
            try
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    if (User.IsInRole("Admin"))
                //    { 
                if (ModelState.IsValid)
                {
                    if (model.ProductId == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    } 
                     await _productDetail.AddAllProductDetailsAsync(model.ProductDetails);
                    _uow.SaveAllChanges();
                    return RedirectToAction("Index");
                }
                return View(model);
                //    }
                //}
                //return RedirectToAction("login", "Account");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public async Task<ActionResult> Edit(int? id)
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    if (User.IsInRole("Admin"))
            //    {

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
            //        }
            //    }
            //    return RedirectToAction("login", "Account");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductDetailDTO productDetail)
        {
            try
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    if (User.IsInRole("Admin"))
                //    {
                if (ModelState.IsValid)
                {
                    await _productDetail.UpdateProductDetailAsync(productDetail);
                }
                return RedirectToAction("Index");
                // }
                //    }
                //    return RedirectToAction("login", "Account");
            }
            catch (Exception)
            {
                return View("Index");
            }
        }
        public async Task<ActionResult> EditAll(int productId)
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    if (User.IsInRole("Admin"))
            //    {

            if (productId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productDetail = await _productDetail.GetAllProductDetailByProductIdAsync(productId);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            return View(productDetail);
            //        }
            //    }
            //    return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAll(List<ProductDetailDTO> productDetails)
        {
            try
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    if (User.IsInRole("Admin"))
                //    {
                if (ModelState.IsValid)
                {
                    await _productDetail.UpdateAllProductDetailAsync(productDetails);
                }
                return RedirectToAction("Index");
                // }
                //    }
                //    return RedirectToAction("login", "Account");
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