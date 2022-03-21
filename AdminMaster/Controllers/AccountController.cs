using AdminMaster.Repository.Interface;
using AdminMaster.Utils.Enums;
using AdminMaster.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdminMaster.Controllers
{
    public class AccountController : Controller
    {
        private IUsers userservice; // create variable
        public AccountController(IUsers users)
        {
            userservice = users;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel model) // means async Task<IActionResult> actiom
        {
            if (ModelState.IsValid)
            {
                var result = userservice.SingIn(model);
                if (result == SignInEnum.Success)
                {
                    //A claim is a statement about a subject by an issuer and    
                    //represent attributes of the subject that are useful in the context of authentication and authorization operations.    
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name,model.Email),
                    
                };
                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    //async
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = model.RememberMe
                    });


                    return RedirectToAction("Index", "Home");
                }
                else if(result == SignInEnum.WrongCredentials)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Credentials !");
                }
                else if (result == SignInEnum.NotVerified)
                {
                    ModelState.AddModelError(string.Empty, "User not varified please varified first !");
                }
                else if (result == SignInEnum.InActive)
                {
                    ModelState.AddModelError(string.Empty, "Your account is Inactive !");
                }  
            }
            return View(model); 
        }
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost] 
        public IActionResult Register(SignUpModel signUp)
        {
            if (ModelState.IsValid)
            {
                var result = userservice.SingUp(signUp);
                if (result == SignUpEnum.Success)
                {
                    return RedirectToAction("Verified_Account");
                }
                else if (result == SignUpEnum.EmailExist)
                {
                    ModelState.AddModelError(string.Empty, "Email already exist, Please try another !");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong !");
                }
            }
            return View(signUp);
        }

        public IActionResult Verified_Account()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Verified_Account(string Otp)
        {
            if(Otp != null)
            {
                if (userservice.VerifyAccount(Otp))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid OTP !");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please Enter OTP !");
            }
            return View();
        }
    }
}
