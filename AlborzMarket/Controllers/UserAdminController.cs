﻿using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Entities;
using Alborz.ServiceLayer.IService;
using AlborzMarket.Models; 
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AlborzMarket.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersAdminController : Controller
    {
        private readonly IApplicationRoleManager _roleManager;
        private readonly IApplicationUserManager _userManager;
        public UsersAdminController(IApplicationUserManager userManager,
                                    IApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await _roleManager.GetAllCustomRolesAsync().ConfigureAwait(false), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = userViewModel.UserName, Email = userViewModel.UserName };
                var adminresult = await _userManager.CreateAsync(user, userViewModel.Password).ConfigureAwait(false);

                //Add User to the selected Roles
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await _userManager.AddToRolesAsync(user.Id, selectedRoles).ConfigureAwait(false);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await _roleManager.GetAllCustomRolesAsync().ConfigureAwait(false), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(await _roleManager.GetAllCustomRolesAsync().ConfigureAwait(false), "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(await _roleManager.GetAllCustomRolesAsync().ConfigureAwait(false), "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _userManager.FindByIdAsync(id.Value).ConfigureAwait(false);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await _userManager.FindByIdAsync(id.Value).ConfigureAwait(false);
                if (user == null)
                {
                    return HttpNotFound();
                }

                var result = await _userManager.DeleteAsync(user).ConfigureAwait(false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _userManager.FindByIdAsync(id.Value).ConfigureAwait(false);

            ViewBag.RoleNames = await _userManager.GetRolesAsync(user.Id).ConfigureAwait(false);

            return View(user);
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _userManager.FindByIdAsync(id.Value).ConfigureAwait(false);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user.Id).ConfigureAwait(false);

            return View(new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                RolesList = (await _roleManager.GetAllCustomRolesAsync().ConfigureAwait(false)).Select(x => new SelectListItem
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(editUser.Id).ConfigureAwait(false);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.UserName;
                user.Email = editUser.Email;

                var userRoles = await _userManager.GetRolesAsync(user.Id).ConfigureAwait(false);

                selectedRole = selectedRole ?? new string[] { };

                var result = await _userManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray()).ConfigureAwait(false);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                await _userManager.UpdateSecurityStampAsync(user.Id).ConfigureAwait(false);

                result = await _userManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray()).ConfigureAwait(false);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                await _userManager.UpdateSecurityStampAsync(user.Id).ConfigureAwait(false);

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/
        public async Task<ActionResult> Index()
        {
            return View(await _userManager.GetAllUsersAsync().ConfigureAwait(false));
        }


        public ActionResult AdminUsers()
        {
            return View(_roleManager.GetApplicationUsersInRole("Admin"));
        }
    }
}
