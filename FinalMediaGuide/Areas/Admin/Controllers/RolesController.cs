﻿using FinalMediaGuide.BLL.ViewModels;
using FinalMediaGuide.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FinalMediaGuide.Areas.Admin.Controllers
{
    [Authorize(Roles = "moderator")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole<int>> _roleManager;
        UserManager<User> _userManager;
        public RolesController(RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole<int>(name));
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else 
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        { 
            IdentityRole<int> role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            { 
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() 
        {
            return View(_userManager.Users.ToList());
        }

        public async Task<IActionResult> Edit(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null) 
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleVM model = new ChangeRoleVM 
                {
                    UserId = userId,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null) 
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var allRoles = _roleManager.Roles.ToList();

                var addedRoles = roles.Except(userRoles);

                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }
            return NotFound();
        }   
    }
}
