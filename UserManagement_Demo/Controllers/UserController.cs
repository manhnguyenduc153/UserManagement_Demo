using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement_Demo.DTOs;
using UserManagement_Demo.Services.IServices;

namespace UserManagement_Demo.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> List(UserSearchDTO search)
        {
            var lstUser = await _userService.GetListPaging(search);
            return View(lstUser);
        }


        [HttpPost]
        public async Task<IActionResult> Add(UserSaveDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _userService.AddAsync(model);

            if (success)
                TempData["SuccessMessage"] = "User added successfully!";
            else
                TempData["ErrorMessage"] = "Failed to add user.";

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserSaveDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _userService.UpdateAsync(model);

            if (success)
                TempData["SuccessMessage"] = "User updated successfully!";
            else
                TempData["ErrorMessage"] = "Failed to update user.";

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteAsync(id);

            if (success)
                TempData["SuccessMessage"] = "User deleted successfully!";
            else
                TempData["ErrorMessage"] = "Failed to delete user.";

            return RedirectToAction("List");
        }
    }
}