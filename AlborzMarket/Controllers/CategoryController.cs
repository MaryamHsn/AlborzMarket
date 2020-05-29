using Alborz.DataLayer.Context;
using Alborz.ServiceLayer.IService;
using AlborzMarket.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace AlborzMarket.Controllers
{
    public class CategoryController : Controller
    {

        readonly ICategoryService _Category;
        readonly IUnitOfWork _uow;
        private CategoryViewModel common;
        private List<CategoryViewModel> commonList;

        public CategoryController(IUnitOfWork uow, ICategoryService Category)
        {
            _Category = Category;
            _uow = uow;
        }

        public async Task<ActionResult> Details(int? id,
            CancellationToken ct = default(CancellationToken))
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var obj =  _Category.GetCategoryAsync(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        //[Authorize(Roles = "admin , SuperViser")]
        //public ActionResult Create()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        if (User.IsInRole("Admin"))
        //        {
        //            common = new CategoryViewModel();
        //            common.RoutList = _rout.GetAllRouts();
        //            common.DriverList = _driver.GetAllDrivers().Select(BaseMapper<DriverTbl, DriverViewModel>.Map).ToList();
        //            return View(common);
        //        }
        //    }
        //    return RedirectToAction("login", "Account");
        //}

        ////[Authorize(Roles = "admin , SuperViser")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(CategoryTbl rout)
        //{
        //    try
        //    {
        //        if (User.Identity.IsAuthenticated)
        //        {
        //            if (User.IsInRole("Admin"))
        //            {
        //                if (ModelState.IsValid)
        //                {
        //                    _Category.AddNewCategory(rout);
        //                    _uow.SaveAllChanges();
        //                    return RedirectToAction("Index");

        //                }
        //                return View();
        //            }
        //        }
        //        return RedirectToAction("login", "Account");
        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        ////[Authorize(Roles = "admin , SuperViser")]
        //public ActionResult Edit(int? id)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        if (User.IsInRole("Admin"))
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            CategoryTbl Category = _Category.GetCategory(id);
        //            if (Category == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            var obj = BaseMapper<CategoryViewModel, CategoryTbl>.Map(Category);
        //            obj.Driver = BaseMapper<DriverViewModel, DriverTbl>.Map(_driver.GetDriver(Category.DriverId));
        //            obj.Rout = _rout.GetRout(Category.RoutId);
        //            obj.DriverList = _driver.GetAllDrivers().Select(BaseMapper<DriverViewModel, DriverTbl>.Map).ToList();
        //            obj.RoutList = _rout.GetAllRouts();
        //            return View(obj);
        //        }
        //    }
        //    return RedirectToAction("login", "Account");
        //}

        ////[Authorize(Roles = "admin , SuperViser")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(CategoryViewModel Category)
        //{
        //    try
        //    {
        //        if (User.Identity.IsAuthenticated)
        //        {
        //            if (User.IsInRole("Admin"))
        //            {
        //                if (ModelState.IsValid)
        //                {
        //                    var obj = BaseMapper<CategoryTbl, CategoryViewModel>.Map(Category);
        //                    obj.IsActive = true;
        //                    _Category.UpdateCategory(obj);
        //                    _uow.SaveAllChanges();
        //                }
        //                return RedirectToAction("Index");
        //            }
        //        }
        //        return RedirectToAction("login", "Account");
        //    }
        //    catch (Exception)
        //    {
        //        return View("Index");
        //    }
        //}

        ////[Authorize(Roles = "admin , SuperViser")]
        //public ActionResult Delete(int? id)
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
        //                CategoryTbl Category = _Category.GetCategory(id);
        //                if (Category == null)
        //                {
        //                    return HttpNotFound();
        //                }
        //                var obj = BaseMapper<CategoryTbl, CategoryViewModel>.Map(Category);
        //                obj.DriverFullName = _driver.GetDriver(Category.DriverId).FullName;
        //                obj.RoutRegionName = _rout.GetRout(Category.RoutId).RegionTbl.RegionName;
        //                obj.RoutEnterTimeString = _rout.GetRout(Category.RoutId).EnterTime.ToString();
        //                obj.RoutStartDateString = ((DateTime)_rout.GetRout(Category.RoutId).StartDate).ToPersianDateString();
        //                obj.Phone1 = _driver.GetDriver(Category.DriverId).Phone1;
        //                obj.RoutShiftType = _rout.GetRout(Category.RoutId).ShiftType;
        //                obj.RoutShiftType = _rout.GetRout(Category.RoutId).ShiftType;
        //                return View(obj);
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
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        if (User.Identity.IsAuthenticated)
        //        {
        //            if (User.IsInRole("Admin"))
        //            {
        //                _Category.Delete(id);
        //                _uow.SaveAllChanges();
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

       
        ////[Authorize(Roles = "admin , SuperViser")]
        //public ActionResult SearchByRout(int? id, int? page, string dropRegionId)
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
        //                var CategoryList = _Category.GetCategoryByRoutId((int)id);
        //                if (CategoryList == null)
        //                {
        //                    return HttpNotFound();
        //                }
        //                IEnumerable<SelectListItem> regionItems = _region.GetAllRegions().Select(c => new SelectListItem
        //                {
        //                    Value = c.Id.ToString(),
        //                    Text = c.RegionName
        //                });

        //                if (!string.IsNullOrWhiteSpace(dropRegionId))
        //                {
        //                    ViewBag.Region = regionItems.Where(x => x.Value == dropRegionId).FirstOrDefault().Text;
        //                }
        //                else
        //                {
        //                    dropRegionId = ViewBag.Region != null ? ViewBag.Rgion : "0";
        //                }
        //                ViewBag.RegionItems = regionItems;
        //                commonList = new List<CategoryViewModel>();
        //                int pageSize = 10;
        //                var allDrivers = _driver.GetAllDrivers();
        //                var rout = _rout.GetRout(id);
        //                int pageNumber = (page ?? 1);
        //                foreach (var item in CategoryList)
        //                {
        //                    var element = BaseMapper<CategoryViewModel, CategoryTbl>.Map(item);
        //                    element.Driver = BaseMapper<DriverViewModel, DriverTbl>.Map(allDrivers.Where(x => x.Id == item.DriverId).FirstOrDefault());
        //                    element.DriverFullName = element.Driver.FullName;
        //                    if (item.RoutTbl != null)
        //                    {
        //                        element.RoutEnterTimeString = item.RoutTbl.EnterTime.ToString();
        //                        element.RoutStartDate = item.RoutTbl.StartDate;
        //                        element.RoutStartDateString = ((DateTime)item.RoutTbl.StartDate).ToPersianDateString();
        //                        if (item.RoutTbl.RegionTbl != null)
        //                            element.RoutRegionName = item.RoutTbl.RegionTbl.RegionName;
        //                    }
        //                    element.RoutShiftType = _rout.GetRout(element.RoutId).ShiftType;
        //                    commonList.Add(element);
        //                }
        //                return View("Index", commonList.ToPagedList(pageNumber, pageSize));
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SearchByDate(DateTime? fromDate)
        //{
        //    try
        //    {
        //        if (User.Identity.IsAuthenticated)
        //        {
        //            if (User.IsInRole("Admin"))
        //            {
        //                if (fromDate == null)
        //                {
        //                    fromDate = DateTime.Now.Date;
        //                }
        //                var rout = _rout.GetAllRoutsByDate(fromDate);
        //                var routIds = rout.Select(x => x.Id).ToList();
        //                var Categorys = _Category.GetCategoryByRoutIds(routIds);
        //                if (Categorys == null)
        //                {
        //                    return HttpNotFound();
        //                }
        //                return RedirectToAction("Index");
        //            }
        //        }
        //        return RedirectToAction("login", "Account");
        //    }
        //    catch (Exception)
        //    {
        //        return View();
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