using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
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
        public async Task<ActionResult> ShowById(int? id, CancellationToken ct = default(CancellationToken))
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = await _file.GetFileAsync(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }
        public async Task<ActionResult> ShowByEntityEnumKeyId(FileDTO model, CancellationToken ct = default(CancellationToken))
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = await _file.GetFilesByEntityEnumKeysAsync(model.EntityEnumId, model.EntityKeyId);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        public ActionResult Create(int entityEnumId, int entityKeyId)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    common = new FileDTO()
                    {
                        EntityEnumId = entityEnumId,
                        EntityKeyId = entityKeyId,
                        Files = new List<HttpPostedFileBase>()
                    };
                    return View(common);
                }
            }
            return RedirectToAction("login", "Account");
        }
        [HttpPost]
        public ActionResult Create(FileDTO model)
        {
            if (ModelState.IsValid)
            {
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