using Travel.Models.Account;
using Travel.Models.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Services
{
    public interface IPermissionService
    {

       
        #region Roles

        List<Role> GetRoles();
        int AddRole(Role role);
        void AddRolesToUser(List<int> roleIds, int userId);
        void EditRolesUser(List<int> roleIds, int userId);
        Role GetRoleById(int roleId);
        void UpdateRole (Role role);
        void DeleteRole(Role role);
        #endregion

        #region Permission
        List<Permission> GetAllPermission();

        void AddPermissionsToRole(int roleId, List<int> permission);
        List<int> permissionsRole(int roleId);
        void UpdatePermissions(int roleId, List<int>permissions);
        bool CheckPermission(int permissionId, string userName);
     
        #endregion
    }
}
