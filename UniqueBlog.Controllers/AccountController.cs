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

namespace UniqueBlog.Controllers
{
	[Export]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public class AccountController : Controller
	{
		//[Import(typeof(IAccountService))]
		//public IAccountService AccountService { get; set; }

		public IAccountService AccountService { get; set; }

		[ImportingConstructor]
		public AccountController(IAccountService accountService)
		{
			this.AccountService = accountService;
		}

		[HttpGet]
		public ActionResult Login()
		{
			ViewBag.Title = Constants.ConstantData.TitleOfLogin;
			ViewBag.ProductName = Constants.ConstantData.ProductName;
			UserDto user = new UserDto();
			return View();
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
