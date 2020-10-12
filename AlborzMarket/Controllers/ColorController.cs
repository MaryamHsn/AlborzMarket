using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
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
    public class ColorController : Controller
    {
        readonly IColorService _color;
        readonly IUnitOfWork _uow;
        private ColorDTO common;
        private List<ColorDTO> commonList;

        public ColorController(IUnitOfWork uow,IColorService Color)
        {
            _uow = uow;
            _color = Color;
        }
        [HttpGet]
        public async Task<ActionResult> Index(ColorDTO model)
        {
            commonList = new List<ColorDTO>();
            ViewBag.CurrentSort = model.SortOrder;
            ViewBag.Title = model.SortOrder == "code" ? "title_desc" : "title"; 
            var color = new List<ColorDTO>();

            if (model.CurrentFilter == null)
            {
                model.Page = 1;
                 color = await _color.GetAllColorsAsync();

            }
            else
            {
                model.SearchString = model.CurrentFilter;
                color = await _color.GetColorsBySearchItemAsync(model.SearchString);

            }

            switch (model.SortOrder)
            {
                case "title_desc":
                    color = color.OrderByDescending(s => s.Title).ToList();
                    break; 
                default:
                    color = color.OrderBy(s => s.Title).ToList();
                    break;
            }
            int pageSize = 30;
            int pageNumber = (model.Page ?? 1); 
            model.ColorPageList = color.ToPagedList(pageNumber, pageSize);
            return View(model);
        }
        public async Task<ActionResult> Create()
        { 
            return View(common); ;
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ColorDTO color)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    { 
                        if (ModelState.IsValid)
                        {
                            await _color.AddNewColorAsync(color);
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
                    var color= await _color.GetColorAsync(id);
                    if (color== null)
                    {
                        return HttpNotFound();
                    } 
                    return View(color);
                }
            }
            return RedirectToAction("login", "Account");
        }

        //[Authorize(Roles = "admin , SuperViser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ColorDTO color, string returnController, string returnAction)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    { 
                        await _color.UpdateColorAsync(color); 
                    }
                    if (!string.IsNullOrEmpty(returnController) && !string.IsNullOrEmpty(returnAction))
                    {
                        return RedirectToAction(returnAction, returnController, new { id = color.Id });
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


    }
}