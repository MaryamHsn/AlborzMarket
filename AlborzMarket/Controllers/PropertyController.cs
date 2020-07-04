using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using Alborz.ServiceLayer.Enumration;
using Alborz.ServiceLayer.IService;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AlborzMarket.Controllers
{
    public class PropertyController : Controller
    {

        readonly IPropertyService _property;
        readonly IProductService _product;
        readonly ICategoryService _category;
        readonly IUnitOfWork _uow;
        private PropertyDTO common;
        private List<PropertyDTO> commonList;

        public PropertyController(IUnitOfWork uow, IPropertyService property, ICategoryService category, IProductService product)
        {
            _uow = uow;
            _product = product;
            _category = category;
            _property = property;
        }
        [HttpGet]
        public async Task<ActionResult> PropertyListByProductId(PropertyDTO model)
        {
            var properties = await _property.GetAllPropertiesByProductIdAsync(model.ProductId); 
            int pageSize = 30;
            int pageNumber = (model.Page ?? 1);
            model.PropertiesList = properties.ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        ////[Authorize(Roles = "admin , SuperViser")]
        public ActionResult CreatePropertyForProduct(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    common = new PropertyDTO()
                    {
                        ProductId = id,
                        Properties = new List<PropertyDTO>()
                    };
                    return View(common);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePropertyForProduct(PropertyDTO model)
        {
            try
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    if (User.IsInRole("Admin"))
                //    { 
                if (ModelState.IsValid)
                {
                    if (model.ProductId == 0)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    foreach (var item in model.Properties)
                    {
                        item.ProductId = model.ProductId;
                        var productInfo= await _product.GetProductAsync(model.ProductId);
                        item.CategoryId = productInfo.CategoryId;
                    }
                    await _property.AddAllPropertiesAsync(model.Properties.ToList());
                    _uow.SaveAllChanges();

                    var t = (int)FileEntityEnum.Product;
                    return RedirectToAction("Create", "File", new { entityEnumId=(int)FileEntityEnum.Product, entityKeyId = model.ProductId });

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
        public ActionResult CreatePropertyForCategory()
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
        public async Task<ActionResult> CreatePropertyForCategory(PropertyDTO model)
        {
            try
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    if (User.IsInRole("Admin"))
                //    { 
                if (ModelState.IsValid)
                {
                    if (model.CategoryId == 0)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    await _property.AddAllPropertiesAsync(model.Properties.ToList());
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
            var property = await _property.GetPropertyAsync(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
            //        }
            //    }
            //    return RedirectToAction("login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PropertyDTO property)
        {
            try
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    if (User.IsInRole("Admin"))
                //    {
                if (ModelState.IsValid)
                {
                    await _property.UpdatePropertyAsync(property);
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
            var property = await _property.GetAllPropertiesByProductIdAsync(productId);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
            //        }
            //    }
            //    return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAll(List<PropertyDTO> propertys)
        {
            try
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    if (User.IsInRole("Admin"))
                //    {
                if (ModelState.IsValid)
                {
                    await _property.UpdateAllPropertiesAsync(propertys);
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
                        var property = await _property.GetPropertyAsync(id);
                        if (property == null)
                        {
                            return HttpNotFound();
                        }
                        return View(property);
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
                        await _property.DeleteAsync(id);
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
                        await _property.DeleteByProductIdAsync(productId);
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
        public ActionResult PropertyEntry()
        {
            return PartialView("PropertyEntry");
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