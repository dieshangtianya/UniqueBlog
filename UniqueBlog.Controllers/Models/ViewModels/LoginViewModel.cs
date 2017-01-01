using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UniqueBlog.Controllers.Models.ViewModels
{
	public class LoginViewModel : PageViewModelBase
	{
		public string Title { get; set; }

		public string ProductName { get; set; }

		[Required(ErrorMessage = "请输入用户名")]
		public string UserName { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "请输入密码")]
		public string Password { get; set; }

		public string LoginError { get; set; }


		public LoginViewModel()
		{
			this.ProductName = Constants.ConstantData.ProductName;
			this.Title = Constants.ConstantData.TitleOfLogin;
			this.LoginError = "";
		}
	}
}
