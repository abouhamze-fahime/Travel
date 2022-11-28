using Travel.TravelDbContext;
using Travel.Convertor;

using Travel.Models.Account;
using Travel.Generator;
using Travel.Models;
using Travel.Models.UserModel;
using Travel.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Travel.Services;


namespace Travel.Services
{
    public class UserService : IUserService
    {
        private TrDbContext _context;
        public UserService(TrDbContext context)
        {
            _context = context;
        }
      
        public bool IsExistEmail(string email)
        {
            return _context.tblUser.Any(u => u.UserName == email);
        }
        public bool IsExistUserName(string userName)
        {
            return _context.tblUser.Any(u => u.UserName == userName);
        }
        public int AddUser(User user)
        {
            _context.tblUser.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }
        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string userName =login.UserName;
            var us= _context.tblUser.SingleOrDefault(u => u.UserName == userName && u.Password == hashPassword);
            return us;
        }

        public bool ActiveAccount(string activeCode)
        {
            var user = _context.tblUser.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
                return false;
            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            _context.SaveChanges();
            return true;

        }
        public User GetUserByEmail(string email)
        {
            return _context.tblUser.SingleOrDefault(u => u.Email == email);
        }
        public User GetUserById(int userId)
        {
            return _context.tblUser.Find(userId);
        }
        public User GetUserByActiveCode(string activeCode)
        {
            return _context.tblUser.SingleOrDefault(u => u.ActiveCode == activeCode);
        }
        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }
        public User GetUserByUserName(string username)
        {
            return _context.tblUser.SingleOrDefault(u => u.UserName == username);
        }
        public InformationUserViewModel GetUserInformation(string username)
        {
            var user = GetUserByUserName(username);
            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.Wallet = 0;

            return information;

        }
        public InformationUserViewModel GetUserInformation(int userId)
        {
            var user = GetUserById(userId);
            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.Wallet = 0;

            return information;
        }
        public UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> res = _context.tblUser.Where(x=>!x.IsDelete);
            if (!string.IsNullOrEmpty(filterEmail))
            {
                res = res.Where(u => u.Email.Contains(filterEmail));
            }
            if (!string.IsNullOrEmpty(filterUserName))
            {
                res = res.Where(u => u.UserName.Contains(filterUserName));
            }
            //Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;
            UserForAdminViewModel lst = new UserForAdminViewModel();
            lst.CurrentPage = pageId;
            lst.PageCount = res.Count() / take;
            lst.Users = res.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();
            return lst;
        }
        public int AddUserFromAdmin(CreateUserViewModel user)
        {
            User addUser = new User();
            addUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            addUser.ActiveCode = NameGenerator.GenerateUniqCode();
            addUser.Email = user.Email;
            addUser.IsActive = true;
            addUser.RegisterDate = DateTime.Now;
            addUser.UserName = user.UserName;
            #region Save Avatar

            if (user.UserAvatar != null)
            {
                string imagePath = "";
                addUser.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/dist/img", addUser.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }
            }

            #endregion

            return AddUser(addUser);
        }
        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
           var a =  _context.tblUser.Where(r => r.UserId == userId)
                                  .Select(u => new EditUserViewModel()
                                  {
                                      UserId = u.UserId,
                                      UserName = u.UserName,
                                      AvatarName = u.UserAvatar,
                                      Email = u.Email,
                                      UserRole = u.UserRoles.Select(r => r.RoleId).ToList()
                                  }).Single();
            return a;
        }
        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            //TblUser user = GetUserById(editUser.UserId);
            //user.Email = editUser.Email;
            //if (!string.IsNullOrEmpty(editUser.Password))
            //{
            //    user.Password= PasswordHelper.EncodePasswordMd5(editUser.Password);
            //}
            //if (editUser.UserAvatar != null)
            //{
            //    //delete old image
            //    if (editUser.UserName!="Default.jpg")
            //    {
            //          string deletePath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editUser.AvatarName);
            //        if (File.Exists(deletePath))
            //        {
            //            File.Delete(deletePath);
            //        }
            //    }
            //    //Save New Image
            //    user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(editUser.UserAvatar.FileName);
            //    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.UserAvatar);
            //    using (var stream = new FileStream(imagePath, FileMode.Create))
            //    {
            //        editUser.UserAvatar.CopyTo(stream);
            //    }
            //}

            //_context.TblUser.Update(user);
            //_context.SaveChanges();


            User user = GetUserById(editUser.UserId);
            user.Email = editUser.Email;
            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
            }

            if (editUser.UserAvatar != null)
            {
                //Delete old Image
                if (editUser.AvatarName != "Defult.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/dist/img", editUser.AvatarName);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }
                //Save New Image
                user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(editUser.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/dist/img", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editUser.UserAvatar.CopyTo(stream);
                }
            }

            _context.tblUser.Update(user);
            _context.SaveChanges();
        }
        public UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> res = _context.tblUser.IgnoreQueryFilters().Where(u=>u.IsDelete);
            if (!string.IsNullOrEmpty(filterEmail))
            {
                res = res.Where(u => u.Email.Contains(filterEmail));
            }
            if (!string.IsNullOrEmpty(filterUserName))
            {
                res = res.Where(u => u.UserName.Contains(filterUserName));
            }
            //Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;
            UserForAdminViewModel lst = new UserForAdminViewModel();
            lst.CurrentPage = pageId;
            lst.PageCount = res.Count() / take;
            lst.Users = res.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();
            return lst;
        }
        public void DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }






        public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
        {
            return _context.tblUser.Where(u => u.UserName == username).Select(u => new SideBarUserPanelViewModel()
            {
                UserName = u.UserName,
                ImageName = u.UserAvatar,
                RegisterDate = u.RegisterDate
            }).Single();
        }

        public EditProfileViewModel GetDataForEditProfileUser(string username)
        {
            return _context.tblUser.Where(u => u.UserName == username).Select(u => new EditProfileViewModel()
            {
                AvatarName = u.UserAvatar,
                Email = u.Email,
                UserName = u.UserName

            }).Single();
        }

        public void EditProfile(string username, EditProfileViewModel profile)
        {
            if (profile.UserAvatar != null)
            {
                string imagePath = "";
                if (profile.AvatarName != "Defult.jpg")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                profile.AvatarName = NameGenerator.GenerateUniqCode() + Path.GetExtension(profile.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", profile.AvatarName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    profile.UserAvatar.CopyTo(stream);
                }

            }

            var user = GetUserByUserName(username);
            user.UserName = profile.UserName;
            user.Email = profile.Email;
            user.UserAvatar = profile.AvatarName;

            UpdateUser(user);

        }

        public bool CompareOldPassword(string oldPassword, string username)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.tblUser.Any(u => u.UserName == username && u.Password == hashOldPassword);
        }

        public void ChangeUserPassword(string userName, string newPassword)
        {
            var user = GetUserByUserName(userName);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }


    }
}
