using Alborz.DataLayer.Context;
using Alborz.DomainLayer.Entities;
using Alborz.ServiceLayer.IService;
using Alborz.ServiceLayer.Mapper;
using AlborzMarket.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using Alborz.ServiceLayer.Utils;

namespace AlborzMarket.Controllers
{
    public class CategoryController : Controller
    {

        readonly ICategoryService _category;
        readonly IUnitOfWork _uow;
        private CategoryViewModel common;
        private List<CategoryViewModel> commonList;

        public CategoryController(IUnitOfWork uow, ICategoryService Category)
        {
            _category = Category;
            _uow = uow;
        }
        [HttpGet]
        public async Task<ActionResult> Index(CategoryFullViewModel model)
        {
            commonList = new List<CategoryViewModel>();
            ViewBag.CurrentSort = model.sortOrder;
            ViewBag.Title = String.IsNullOrEmpty(model.sortOrder) ? "title_desc" : "";
            ViewBag.Code = model.sortOrder == "code" ? "code_desc" : "code";
            ViewBag.Priority = model.sortOrder == "priority" ? "priority_desc" : "priority";
            ViewBag.ParentCategory = model.sortOrder == "parentCategory" ? "parentCategory_desc" : "parentCategory";

            if (model.searchString != null)
            {
                model.page = 1;
            }
            else
            {
                model.searchString = model.currentFilter;
            }
            //ViewBag.CurrentFilter = model.searchString;
            var category = await _category.GetAllCategoriesAsync();
            if (!String.IsNullOrEmpty(model.searchString))
            {
                category = category.Where(s => s.Title.Contains(model.searchString)
                                               || s.Code.Contains(model.searchString)
                                               || s.priority.ToString().Contains(model.searchString)).ToList();
            }
            switch (model.sortOrder)
            {
                case "title_desc":
                    //var driver = _driver.GetAllDrivers().OrderByDescending(s => s.DriverId).ToList();
                    category = category.OrderByDescending(s => s.Title).ToList();
                    break;
                case "code":
                    category = category.OrderBy(s => s.Code).ToList();
                    break;
                case "code_desc":
                    category = category.OrderByDescending(s => s.Code).ToList();
                    break;
                case "priority":
                    category = category.OrderBy(s => s.priority).ToList();
                    break;
                case "priority_desc":
                    category = category.OrderByDescending(s => s.priority).ToList();
                    break;
                case "parentCategory":
                    category = category.Where(s => s.ParentCategoryId != null).OrderBy(s => s.Title).ToList();
                    break;
                case "parentCategory_desc":
                    category = category.Where(s => s.ParentCategoryId != null).OrderByDescending(s => s.Title).ToList();
                    break;
                default:
                    category = category.OrderBy(s => s.Title).ToList();
                    break;
            }
            int pageSize = 100;
            int pageNumber = (model.page ?? 1);
            foreach (var item in category)
            {
                var element = BaseMapper<CategoryViewModel, CategoryTbl>.Map(item);
                element.StartDateString = ((DateTime)(item.StartDate)).ToPersianDateString();
                element.EndDateString = ((DateTime)(item.EndDate)).ToPersianDateString();
                commonList.Add(element);
            }
            model.CategoryViewModels = commonList.ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        public async Task<ActionResult> Details(int? id, CancellationToken ct = default(CancellationToken))
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = _category.GetCategoryAsync(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            var obj = BaseMapper<CategoryViewModel, CategoryTbl>.Map(await entity);
            return View(obj);
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public async Task<ActionResult> Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return View(common);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryTbl rout)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        if (ModelState.IsValid)
                        {
                            await _category.AddNewCategoryAsync(rout);
                            _uow.SaveAllChanges();
                            return RedirectToAction("Index");
                        }
                        return View();
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
        public async Task<ActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    CategoryTbl Category = await _category.GetCategoryAsync(id);
                    if (Category == null)
                    {
                        return HttpNotFound();
                    }
                    var obj = BaseMapper<CategoryViewModel, CategoryTbl>.Map(Category);
                    return View(obj);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryViewModel Category)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        if (ModelState.IsValid)
                        {
                            var obj = BaseMapper<CategoryTbl, CategoryViewModel>.Map(Category);
                            obj.IsActive = true;
                            await _category.UpdateCategoryAsync(obj);
                            _uow.SaveAllChanges();
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
                        CategoryTbl category = _category.GetCategory(id);
                        if (category == null)
                        {
                            return HttpNotFound();
                        }
                        var obj = BaseMapper<CategoryTbl, CategoryViewModel>.Map(category);
                        obj.StartDateString = ((DateTime)(category.StartDate)).ToPersianDateString();
                        obj.EndDateString = ((DateTime)(category.EndDate)).ToPersianDateString();
                        return View(obj);
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
                        _category.Delete(id);
                        _uow.SaveAllChanges();
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