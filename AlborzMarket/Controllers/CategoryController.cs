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
using Alborz.DomainLayer.DTO;

namespace AlborzMarket.Controllers
{
    public class CategoryController : Controller
    {

        readonly ICategoryService _category;
        readonly IUnitOfWork _uow;
        private CategoryDTO common;
        private List<CategoryDTO> commonList;

        public CategoryController(IUnitOfWork uow, ICategoryService Category)
        {
            _category = Category;
            _uow = uow;
        }
        [HttpGet]
        public async Task<ActionResult> Index(CategoryDTO model)
        {
            commonList = new List<CategoryDTO>();
            ViewBag.CurrentSort = model.sortOrder;
            ViewBag.Title = model.sortOrder == "code" ? "title_desc" : "title";
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
            var category = new List<CategoryDTO>();
            if (!String.IsNullOrEmpty(model.searchString))
            {
                category = await _category.GetCategoriesBySearchItemAsync(model.searchString);
            }
            else
            {
                category = await _category.GetAllCategoriesAsync();
            }
            switch (model.sortOrder)
            {
                case "title_desc":
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
            //foreach (var item in category)
            //{
            //    var element = BaseMapper<CategoryViewModel, CategoryTbl>.Map(item);
            //    element.StartDateString = ((DateTime)(item.StartDate)).ToPersianDateString();
            //    element.EndDateString = ((DateTime)(item.EndDate)).ToPersianDateString();
            //    commonList.Add(element);
            //}
            model.Categories = await _category.GetAllCategoriesAsync();
            model.CategoryPageList = category.ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        public async Task<ActionResult> Details(int? id, CancellationToken ct = default(CancellationToken))
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = await _category.GetCategoryAsync(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        //[Authorize(Roles = "admin , SuperViser")]
        public async Task<ActionResult> Create()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    if (User.IsInRole("Admin"))
            //    {
            common = new CategoryDTO()
            {
                Categories = await _category.GetAllCategoriesAsync()
            };
            return View(common);
            //    }
            //}
            //return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryDTO rout)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        rout.Categories = await _category.GetAllCategoriesAsync();
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
            var category = await _category.GetCategoryAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            category.Categories = await _category.GetAllCategoriesAsync();
            return View(category);
            //        }
            //    }
            //    return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryDTO category)
        {
            try
            {
                //if (User.Identity.IsAuthenticated)
                //{
                //    if (User.IsInRole("Admin"))
                //    {

                category.Categories = await _category.GetAllCategoriesAsync();
               
                if (ModelState.IsValid)
                        {
                            await _category.UpdateCategoryAsync(category);
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
                        var category = await _category.GetCategoryAsync(id);
                        category.Categories = await _category.GetAllCategoriesAsync();
                        if (category == null)
                        {
                            return HttpNotFound();
                        }
                        return View(category);
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
                        await _category.DeleteAsync(id);
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