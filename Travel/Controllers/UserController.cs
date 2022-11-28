using Microsoft.AspNetCore.Mvc;
using Travel.Models.Account;
using Travel.Models.Permission;
using Travel.Models.UserModel;
using Travel.Security;
using Travel.Services;

namespace Travel.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        public UserController(IUserService userService , IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }



        [PermissionChecker(4)]
        public IActionResult GetUser(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
           var   UserForAdmin = _userService.GetUsers(pageId, filterUserName, filterEmail);
            return View("Index" , UserForAdmin);
        }


        [HttpGet]
        public IActionResult CreateUser()
        {
            ViewData["Roles"] = _permissionService.GetRoles();

            return View("CreateUser");
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserViewModel user  )
        {
            int userId = _userService.AddUserFromAdmin(user);
            //Add Roles
            _permissionService.AddRolesToUser(user.SelectedRoles, userId);
            return RedirectToAction(nameof(GetUser));
        }

 
        public IActionResult EditUser(int id)
        {
           var   editUser = _userService.GetUserForShowInEditMode(id);
            var a = _permissionService.GetRoles();
            ViewData["Roles1"] = a;
            return View("EditUser" , editUser);
        }

        [HttpPost]
        public IActionResult EditUser(EditUserViewModel user)
        {
             _userService.EditUserFromAdmin(user);
            //Add Roles
            _permissionService.EditRolesUser(user.UserRole, user.UserId);
            return RedirectToAction(nameof(GetUser));
        }
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
             return Ok();
        }

        [HttpGet]
        [PermissionChecker(5)]
        public IActionResult GetListDeleteUsers(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
           var    UserForAdmin = _userService.GetDeleteUsers(pageId, filterUserName, filterEmail);
            return View("ListDeleteUsers", UserForAdmin);
        }


    }
}
