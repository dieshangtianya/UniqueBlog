﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Data;
using UniqueBlog.Domain.Repository;
using UniqueBlog.Domain.Entities;
using UniqueBlog.DBManager;
using UniqueBlog.Infrastructure.Query;
using UniqueBlog.Infrastructure.UnitOfWork;
using UniqueBlog.Infrastructure;

namespace UniqueBlog.Repository
{
    [Export(typeof(IUserRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserRepository : IUserRepository, IUnitOfWorkRepository
    {
        private IDatabase _dbbase;

        private IUnitOfWork _unitOfWork;

        private const string _baseSql = "SELECT * FROM t_user";
        private const string getUserById = "SELECT * FROM t_user where UserId=@UserId";

        [ImportingConstructor]
        public UserRepository(IUnitOfWork unitOfWork)
        {
            this._dbbase = DatabaseFactory.CreateDataBase();
            this._unitOfWork = unitOfWork;
        }

        #region IUserRepository implement

        public IEnumerable<User> FindAll()
        {
            throw new NotImplementedException();
        }

        public PagedResult<User> FindAll(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FindBy(Query query)
        {
            List<User> userList = new List<User>();

            using (var connection = _dbbase.CreateDbConnection())
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                query.TranslateIntoSql(command, _baseSql);
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = this.GetUserFromReader(reader);
                        userList.Add(user);
                    }
                }
            }

            return userList;
        }

        public PagedResult<User> FindBy(Query query, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public User FindBy(int entityId)
        {
            IDbConnection connection = _dbbase.CreateDbConnection();
            IDbCommand command = connection.CreateCommand();
            command.CommandText = getUserById;
            IDbDataParameter parameter = _dbbase.CreateDbParameter("UserId", entityId);
            command.Parameters.Add(parameter);

            User user = null;
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = this.GetUserFromReader(reader);
                    break;
                }
            }

            return user;
        }


        public void Add(User entity)
        {
            this._unitOfWork.RegisterNew(entity, this);
        }

        public void Save(User entity)
        {
            this._unitOfWork.RegisterAmended(entity, this);
        }

        public void Remove(User entity)
        {
            this._unitOfWork.RegisterRemoved(entity, this);
        }

        #endregion

        #region IUnitOfWorkRepository

        public void PersistCreationOf(Infrastructure.IAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public void PersistUpdateOf(Infrastructure.IAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public void PersistDeleteOf(Infrastructure.IAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region private methods

        /// <summary>
        /// 通过IDataReader创建User信息
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>User信息</returns>
        private User GetUserFromReader(IDataReader reader)
        {
            int userId = (int)reader["UserId"];
            User user = new User(userId);
            user.UserName = reader["UserName"].ToString();
            user.NickName = reader["NickName"].ToString();
            user.Email = reader["Email"].ToString();
            user.Password = reader["Email"].ToString();

            return user;
        }
        #endregion
    }
}
