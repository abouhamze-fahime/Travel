using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Travel.Models.Account;
using Travel.Models.Permission;
using Travel.Models.UserModel;
using Travel.Security;
using Travel.Services;

namespace Travel.Controllers
{
    
    public class AdminController : Controller
    {
       
        private IPermissionService _permissionService;
        public AdminController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [PermissionChecker(12)]
        public IActionResult Roles()
        {
            var  RoleList = _permissionService.GetRoles().Where(x=>!x.IsDelete).ToList();
            return View("Index" , RoleList);
        }


        //public UserForAdminViewModel UserForAdmin { get; set; }
        //public IActionResult GetUser(int pageId = 1, string filterUserName = "", string filterEmail = "")
        //{
        //    UserForAdmin = _permissionService.GetUsers(pageId, filterUserName, filterEmail);
        //    return View("Index");
        //}



        [HttpGet]
        public IActionResult CreateRoleGet()
        {
            var roles = _permissionService.GetAllPermission();
            ViewData["Permissions"] = roles;
           
            return View("CreateRole" , new Travel.Models.Account.Role());

        }

        [HttpPost]
        public IActionResult CreateRole(CreateRoleViewModel model)
        {
           
            int roleId = _permissionService.AddRole(new Role { RoleTitle=model.RoleTitle});
            if(model.SelectedPermission!=null) _permissionService.AddPermissionsToRole(roleId, model.SelectedPermission);
            return RedirectToAction(nameof(Roles));
        }

        [HttpGet]
       
        public IActionResult EditRole(int id)
        {
            var  role = _permissionService.GetRoleById(id);
            ViewData["Permissions"] = _permissionService.GetAllPermission();
            ViewData["SelectedPermissions"] = _permissionService.permissionsRole(id);
            return View("EditRole" ,role);
        }




        [HttpPost]
        public IActionResult EditRolePost(CreateRoleViewModel model)
        {
            var role = _permissionService.GetRoleById(model.RoleId);
            role.RoleTitle = model.RoleTitle;
            _permissionService.UpdateRole(role);
            _permissionService.UpdatePermissions(role.RoleId, model.SelectedPermission);
            return RedirectToAction(nameof(Roles));
        }




        //[HttpGet]
        //public IActionResult DeleteRole(int id)
        //{
        //    var role = _permissionService.GetRoleById(id);
        //    return View("DeleteRole", role);
        //}



        [HttpPost]
        public IActionResult DeleteRole(int id)
        {
            var role = _permissionService.GetRoleById(id);
            _permissionService.DeleteRole(role);
            return Ok();
        }

    }

}
