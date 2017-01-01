using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Mappers;
using UniqueBlog.Domain.Entities;
using UniqueBlog.Domain.Repository;
using UniqueBlog.DTO;
using UniqueBlog.Infrastructure.Log;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Service.Interfaces;
using UniqueBlog.Service.DtoMapper;

namespace UniqueBlog.Service
{
    [Export(typeof(IAccountService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountService : IAccountService
    {
        private IUserRepository _UserRepository;

        private static readonly ILog logger = LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public AccountService(IUserRepository userRepository)
        {
            this._UserRepository = userRepository;
        }

        public UserDto VerifyUser(UserDto userDto)
        {
			UserDto currentUserDto = null;
            try
            {
                Query query = new Query();

                query.Add(Criterion.Create<User>(item => item.UserName, userDto.UserName, CriterionOperator.Equal));
                query.Add(Criterion.Create<User>(item => item.Password, userDto.Password, CriterionOperator.Equal));

                User user = this._UserRepository.FindBy(query).FirstOrDefault();

				if (user != null)
				{
					currentUserDto = user.ConvertTo();
				}
            }

            catch (Exception exception)
            {
                logger.Error("There is an error happen", exception);
            }

			return currentUserDto;
        }
    }
}
