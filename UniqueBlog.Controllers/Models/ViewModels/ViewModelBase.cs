using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UniqueBlog.DTO;

namespace UniqueBlog.Controllers.Models.ViewModels
{
	public class ViewModelBase
	{
		public bool HasUserLogin {
            get {
                return HttpContext.Current.Session[Constants.ConstantData.CurrentUserSessionKey] != null;
            }
        }

        public UserDto CurrentUser { get; set; }

        public ViewModelBase()
        {
            this.CurrentUser = (UserDto)HttpContext.Current.Session[Constants.ConstantData.CurrentUserSessionKey];
        }
	}
}
