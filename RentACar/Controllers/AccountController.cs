using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentACar.Entities.Concrete;
using RentACar.UI.Models;
using RentACar.UI.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace RentACar.UI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<Customer> userManager;
        private SignInManager<Customer> SignInManager;
        private RoleManager<IdentityRole> RoleManager;
        private ISmtpService smtpService;

        public AccountController(UserManager<Customer> userManager, SignInManager<Customer> SignInManager, RoleManager<IdentityRole> RoleManager, ISmtpService smtpService)
        {
            this.RoleManager = RoleManager;
            this.SignInManager = SignInManager;
            this.userManager = userManager;
            this.smtpService = smtpService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(userLoginViewModel.UserName);
                if (user != null)
                {
                    if (!await userManager.IsEmailConfirmedAsync(user))
                    {
                        TempData.Add("VerificationError", "Confirm your email please");
                        return View(userLoginViewModel);
                    }
                    var result = await SignInManager.PasswordSignInAsync(user, userLoginViewModel.Password, userLoginViewModel.RememberMe, true);
                    if (result.IsLockedOut)
                    {
                        TempData.Add("Lockout", "Your account is locked. Due to unsuccessful password attempts.Please relogin later.");
                        return View();
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToAction("TwoFactorCode");
                    }
                    if (result.Succeeded)
                    {

                        if (await userManager.IsInRoleAsync(user, "Customer"))
                        {
                            return RedirectToAction("Index", "Rent");
                        }
                        if (await userManager.IsInRoleAsync(user, "Manager"))
                        {
                            return RedirectToAction("HomePage", "Admin", new { area = "admin" });


                        }

                    }
                }
            }
            TempData.Add("LoginUserNotFound", "Your password or username is invlaid");
            return View(userLoginViewModel);
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByNameAsync(registerViewModel.UserName);
                if (userCheck == null)
                {
                    if (registerViewModel.Password != registerViewModel.PasswordConfirm)
                    {
                        return View();
                    }
                    Customer user = new Customer
                    {
                        Email = registerViewModel.Email,
                        FirstName = registerViewModel.FirstName,
                        LastName = registerViewModel.LastName,
                        BirthDate = registerViewModel.BirthDate,
                        UserName = registerViewModel.UserName
                    };
                    var result = await userManager.CreateAsync(user, registerViewModel.Password);
                    if (result.Succeeded)
                    {

                        if (!await RoleManager.RoleExistsAsync("Manager"))
                        {
                            IdentityRole role = new IdentityRole
                            {
                                Name = "Manager"
                            };
                            var roleResult = await RoleManager.CreateAsync(role);
                            if (!roleResult.Succeeded)
                            {
                                TempData.Add("RoleMessage", String.Format("We can't add the role"));
                                return View(registerViewModel);
                            }
                        }
                        userManager.AddToRoleAsync(user, "Manager").Wait();
                        var emailConfirmCode = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmUrl = Url.Action("ConfirmMail", "Account", new { userId = user.Id, Code = emailConfirmCode });
                        smtpService.AccountConfirmMail(user.Email, user.UserName, confirmUrl);
                        return RedirectToAction("ConfirmMailSend", "Account");

                    }
                }
                else
                {
                    TempData.Add("UserExist", "This username was taken chose diffrent username");
                }


            }
            return View(registerViewModel);
        }
        public IActionResult ConfirmMailSend()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmMail(string userId, string Code)
        {
            if (userId == null || Code == null)
            {
                TempData.Add("CodeError", "Code invlaid");
                return View();
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData.Add("UserNotFound", String.Format("We can't find a user"));
                return View();
            }
            var result = await userManager.ConfirmEmailAsync(user, Code);
            if (result.Succeeded)
            {
                TempData.Add("ConfirmSuccess", String.Format("Verification Success! you can login now!"));
                return View();
            }
            TempData.Add("ConfirmFail", String.Format("FAILED"));
            return View();
        }
        public IActionResult Logout()
        {
            SignInManager.SignOutAsync().Wait();
            return RedirectToAction("index", "Car");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return View();
            }
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                TempData.Add("WithEmailUserNotFound", "If the user exists, then you will receive an email with a password reset link.");
                return RedirectToAction("ForgotPasswordEmailSend");

            }
            var ForgotPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var PasswordUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, Code = ForgotPasswordToken });
            smtpService.ResetPasswordMail(user.Email, user.UserName, PasswordUrl);

            return RedirectToAction("ForgotPasswordEmailSend");
        }
        public IActionResult ForgotPasswordEmailSend()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string userId, string Code)
        {
            if (userId == null || Code == null)
            {
                TempData.Add("CodeError", "Code invlaid");
                return View();
            }
            var user = await userManager.FindByIdAsync(userId);
            var model = new ResetPasswordViewModel
            {
                Code = Code,
                Email = user.Email
            };

            return View(model);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                if (resetPasswordViewModel.ConfirmPassword != resetPasswordViewModel.Password)
                {
                    TempData.Add("PasswordsNotMatch", "Your must enter same password value");
                    return View(resetPasswordViewModel);
                }
                var user = await userManager.FindByEmailAsync(resetPasswordViewModel.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);
                    if (result.Succeeded)
                    {
                        TempData.Add("ResetPasswordSucceed", "Your password  successfully change!");
                        smtpService.PasswordChangedInfo(user.Email, user.UserName);
                    }
                    return RedirectToAction("Login");
                }
                TempData.Add("MailNotFound", String.Format("This mail adress not match with any user"));

            }
            return View(resetPasswordViewModel);
        }
    }
}
