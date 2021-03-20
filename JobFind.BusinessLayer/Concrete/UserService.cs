using JobFind.BusinessLayer.Abstracts;
using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.Entities;
using JobFind.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.BusinessLayer.Concrete
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IMongoRepositoryBase<User> _userRepository;
        #endregion

        #region CTOR
        public UserService(IMongoRepositoryBase<User> userRepository)
        {
            this._userRepository = userRepository;
        }
        #endregion

        #region Methods
        public bool CreateUser(UserDTO model)
        {
            if (model == null)
            {
                return false;
            }
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password

            };
            _userRepository.Create(user);
            return true;
        }
        #endregion

    }
}
