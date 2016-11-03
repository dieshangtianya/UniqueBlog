using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Service.Interfaces;
using UniqueBlog.Domain.Repository;
using UniqueBlog.Domain.Entities;
using UniqueBlog.DTO;
using AutoMapper;
using AutoMapper.Mappers;


namespace UniqueBlog.Service
{
	[Export(typeof(IAccountService))]
	public class AccountService : IAccountService
	{
		private IUserRepository _UserRepository;

		[ImportingConstructor]
		public AccountService(IUserRepository userRepository)
		{
			this._UserRepository = userRepository;
		}

		public bool VerifyUser(UserDto userDto)
		{
			Query query = new Query();

			query.Add(Criterion.Create<User>(item => item.UserName, userDto.UserName, CriterionOperator.Equal));
			query.Add(Criterion.Create<User>(item => item.Password, userDto.Password, CriterionOperator.Equal));

			User user = this._UserRepository.FindBy(query).FirstOrDefault();
			var isExist = false;

			if (user != null)
			{
				isExist = true;
			}

			return isExist;
		}
	}
}
