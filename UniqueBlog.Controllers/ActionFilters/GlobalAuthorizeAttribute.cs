using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UniqueBlog.DTO;

namespace UniqueBlog.Controllers
{
	/// <summary>
	/// Currently authorization become very simple here, just validate the user whether he has login
	/// If an anonymous user visit the resources which are accessed by authentication user, will be directed to another page
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class GlobalAuthorizeAttribute : AuthorizeAttribute
	{
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext==null)
            {
                throw new ArgumentNullException("httpContext");
            }

            UserDto user = httpContext.Session[Constants.ConstantData.CurrentUserSessionKey] as UserDto;

            if(user!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
