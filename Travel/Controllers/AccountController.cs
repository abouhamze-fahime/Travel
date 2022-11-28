using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel.Convertor;
using Travel.Generator;
using Travel.Models.Account;
using Travel.Models.UserModel;
using Travel.Security;
using Travel.Services;

namespace Travel.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRender;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IUserService userService, IViewRenderService viewRender, ILogger<AccountController> logger)
        {
            _userService = userService;
            _viewRender = viewRender;
            _logger = logger;
        }







        #region  Register

        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {


            if (!ModelState.IsValid)
            {
                return View("register");
            }

            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View("register");
            }
            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View("register");
            }

            User user = new User()
            {
                ActiveCode = NameGenerator.GenerateUniqCode(),
                Email = FixedText.FixEmail(register.Email),
                IsActive = true,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar = "DefaultAvatar1.png",
                UserName = register.UserName
            };
            _userService.AddUser(user);
            //#region Send Activation Email
            //string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            //SendEmail.Send(user.Email, "فعالسازی", body);
            //#endregion

            return View("SuccessRegister", user);
        }
        #endregion

        #region Login
        [Route("Login")]
        public IActionResult Login()
        {
            _logger.LogWarning("Login to the system");
            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " this is my error ");
            }
            return View("Login");
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            var user = _userService.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                       new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                       new Claim(ClaimTypes.Name,user.UserName),
                //       new Claim(ClaimTypes.Role,user.UserRoles.FirstOrDefault().Role.RoleTitle)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };

                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("UserName", "حساب کاربری شما فعال نمی باشد");
                }
            }

            ModelState.AddModelError("UserName", "کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
        }
        #endregion

        #region Active Account
        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }
        #endregion


        #region Logout
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion


        #region Forgot Password
        [HttpGet]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
                return View(forgot);

            string userName = forgot.UserName;
            User user = _userService.GetUserByEmail(userName);

            if (user == null)
            {
                ModelState.AddModelError("UserName", "کاربری یافت نشد");
                return View(forgot);
            }
            ///send sms
            //string bodyEmail = _viewRender.RenderToStringAsync("_ForgotPassword", user);
            //SendEmail.Send(user.UserName, "بازیابی حساب کاربری", bodyEmail);
            //ViewBag.IsSuccess = true;

            return View();
        }
        #endregion

        #region Reset Password
        [HttpGet]
        public ActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id
            });
        }


        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)
                return View(reset);

            User user = _userService.GetUserByActiveCode(reset.ActiveCode);

            if (user == null)
                return NotFound();

            string hashNewPassword = PasswordHelper.EncodePasswordMd5(reset.Password);
            user.Password = hashNewPassword;
            _userService.UpdateUser(user);

            return Redirect("/Login");

        }
        #endregion




        [HttpPost]
        public IActionResult DeleteUserPost(int userId)
        {
            _userService.DeleteUser(userId);
            return RedirectToPage("Index");
        }



    }
}
