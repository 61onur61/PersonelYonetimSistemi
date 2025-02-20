﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PersonelYonetimSistemi.Data.Contracts;
using PersonelYonetimSistemi.Common.SessionOperations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PersonelYonetimSistemi.Data.Models;
using PersonelYonetimSistemi.Common.ResultMesajlari;

namespace PersonelYonetimSistemi.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<Personels> _userManager;
        private readonly SignInManager<Personels> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public LoginModel(SignInManager<Personels> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<Personels> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Email Alanı Boş Bırakılamaz")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "Şifre Alanı Boş Bırakılamaz")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    
                    //var user = _userManager.FindByEmailAsync(Input.Email);
                    var user = _unitOfWork.personelRepository.GetFirstOrDefault(p => p.Email == Input.Email.ToLower());
                    var userInfo = new SessionContext()
                    {
                        Email = user.Email,
                        Ad = user.Ad,
                        //Admin Bilgisini Dinamik Olarak Getir.
                        IsAdmin = false,
                        Soyad = user.Soyad,
                        LoginId = user.Id
                    };
                    //Set To User ınfo Session
                    HttpContext.Session.SetString(ResultMesajlari.LoginUserInfo, JsonConvert.SerializeObject(userInfo));
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                   
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
