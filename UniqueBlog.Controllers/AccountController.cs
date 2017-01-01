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
		public new ActionResult Login()
		{
			LoginViewModel loginViewModel = new LoginViewModel();
			return View(loginViewModel);
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel loginViewModel)
		{
			var user = new UserDto();
			user.UserName = loginViewModel.UserName;
			user.Password = loginViewModel.Password;

			var currentUser= this.AccountService.VerifyUser(user);

			if (currentUser != null)
			{
				HttpContext.Session["CurrentUser"] = user;
				return RedirectToAction("Index", "Home");
			}
			else
			{
				loginViewModel.LoginError = "用户名或密码错误";
				return View(loginViewModel);
			}
		}
	}
}
