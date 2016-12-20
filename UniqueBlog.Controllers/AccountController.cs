using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniqueBlog.Service;
using UniqueBlog.Service.Interfaces;
using UniqueBlog.DTO;
using UniqueBlog.Controllers.Models.ViewModels;

namespace UniqueBlog.Controllers
{
	[Export]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public class AccountController : BlogControllerBase
	{
		public IAccountService AccountService { get; set; }

		[ImportingConstructor]
		public AccountController(IAccountService accountService)
		{
			this.AccountService = accountService;
		}

		[HttpGet]
		public ActionResult Login()
		{
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.ProductName = Constants.ConstantData.ProductName;
            loginViewModel.Title = Constants.ConstantData.TitleOfLogin;
            return View(loginViewModel);
		}

		[HttpPost]
		public ActionResult Login(UserDto user)
		{
			var result = this.AccountService.VerifyUser(user);

			if (result)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.LoginError = "用户名或密码错误";
				return View();
			}
		}
	}
}
