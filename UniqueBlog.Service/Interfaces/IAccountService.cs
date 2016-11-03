using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using UniqueBlog.DTO;

namespace UniqueBlog.Service.Interfaces
{
	public interface IAccountService
	{
		bool VerifyUser(UserDto user);
	}
}
