using Travel.TravelDbContext;
using Travel.Models.Account;
using Travel.Models.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Services
{
    public class PermissionService : IPermissionService
    {

        //FruddataContext context = new FruddataContext();
        private TrDbContext _context;
        public PermissionService(TrDbContext context)
        {
            _context = context;
        }
        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (int roleId in roleIds)
            {
                _context.tblUserRole.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }
            _context.SaveChanges();
        }
        public void EditRolesUser(List<int> roleIds, int userId)
        {
            //Delete All Roles User
            _context.tblUserRole.Where(r => r.UserId == userId).ToList().ForEach(r => _context.tblUserRole.Remove(r));

            //Add New Roles
            AddRolesToUser(roleIds, userId);
        }
        public List<Role> GetRoles()
        {
            var a = _context.tblRole.ToList();
            return a;
        }
        public int AddRole(Role role)
        {
            _context.tblRole.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }
        public Role GetRoleById(int roleId)
        {
            return _context.tblRole.Find(roleId);

        }
        public void UpdateRole(Role role)
        {
            _context.tblRole.Update(role);
            _context.SaveChanges();
        }
        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            UpdateRole(role);
        }
        public List<Permission> GetAllPermission()
        {
            return _context.tblPermission.ToList();
          
               
        }
        public void AddPermissionsToRole(int roleId, List<int> permission)
        {
            foreach (var p in permission)
            {
                _context.tblRolePermission.Add(new RolePermission()
                {
                    PermissionId=p,
                    RoleId=roleId
                });
            }
            _context.SaveChanges();
        }
        public List<int> permissionsRole(int roleId)
        {
            return _context.tblRolePermission
                .Where(r => r.RoleId == roleId)
                .Select(r => r.PermissionId).ToList();
        }
        public void UpdatePermissions(int roleId, List<int> permissions)
        {
            _context.tblRolePermission
                 .Where(p => p.RoleId == roleId)
                 .ToList()
                 .ForEach(p => _context.tblRolePermission.Remove(p));
            AddPermissionsToRole(roleId, permissions);
        }
        public bool CheckPermission(int permissionId, string userName)
        {
            int userId = _context.tblUser.Single(u => u.UserName == userName).UserId;
            List<int> UserRole = _context.tblUserRole
                .Where(r => r.UserId == userId)
                .Select(r => r.RoleId).ToList();
            if (!UserRole.Any())
                return false;

            List<int> RolesPermission = _context.tblRolePermission
                .Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId).ToList();

            return RolesPermission.Any(p => UserRole.Contains(p));

        }
    }

}
