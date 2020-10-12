using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using Alborz.ServiceLayer.Enumration;
using Alborz.ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AlborzMarket.Controllers
{
    public class FileController : Controller
    {
        readonly IFileService _file;
        readonly IUnitOfWork _uow;
        private FileDTO common;
        private List<FileDTO> commonList;

        public FileController(IUnitOfWork uow, IFileService file)
        {
            _file = file;
            _uow = uow;
        }
        public async Task<ActionResult> ShowById(int id, CancellationToken ct = default(CancellationToken))
        {
            id = 2;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = await _file.GetFileAsync(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            //return View(entity);
            return File(entity.Content, "image/jpg");
        }
        public async Task<ActionResult> ShowByEntityEnumKeyId(FileDTO model, CancellationToken ct = default(CancellationToken))
        { 
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //model.EntityEnumId
            var entity = await _file.GetFilesByEntityEnumKeysAsync((int)FileEntityEnum.Product, model.EntityKeyId);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return File(entity.FirstOrDefault().Content, "image/jpg");

            //return View(entity);
        }

        public ActionResult CreateForProduct(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    common = new FileDTO()
                    {
                        EntityEnumId = (int)FileEntityEnum.Product,
                        EntityKeyId = id,
                        ProductId =id,
                        Files = new List<HttpPostedFileBase>()
                    };
                    common.ProductId = id;
                    return View(common);
                }
            }
            return RedirectToAction("login", "Account");
        }
        [HttpPost]
        public async  Task<ActionResult> CreateForProduct(FileDTO model)
        {
            if (ModelState.IsValid)
            { 
            var existFile = await _file.GetFilesByEntityEnumKeysAsync((int)FileEntityEnum.Product, model.EntityKeyId);

                if (existFile.Any())
                {
                    foreach (var item in existFile)
                    {
                        item.IsActive = false;
                        await _file.DeleteAsync(item.Id);

                    }
                }
                foreach (var file in model.Files)
                {
                    //file.File.SaveAs(Path.Combine(Server.MapPath("/uploads"), Guid.NewGuid() + Path.GetExtension(file.Title)));
                    if (file != null && file.ContentLength > 0)
                    {
                        var doc = new FileDTO()
                        {
                            EntityEnumId = model.EntityEnumId,
                            EntityKeyId = (int)model.EntityKeyId,
                            Title = file.FileName,
                            Content = _file.ConvertHttpPostedFileBaseToByte(file),

                            IdFile = Guid.NewGuid(),
                            IsActive = true
                        };
                        _file.AddNewFileAsync(doc);
                    }
                }
                _uow.SaveAllChanges();
                return RedirectToAction("Index", "product");
            }

            return View();
        }

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
                        var file = await _file.GetFileAsync(id);
                        if (file == null)
                        {
                            return HttpNotFound();
                        }
                        return View(file);
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
                        await _file.DeleteAsync(id);
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
        public ActionResult FileUploadEntry()
        {
            return PartialView("FileUploadEntry");
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