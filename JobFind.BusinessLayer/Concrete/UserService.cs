using JobFind.BusinessLayer.Abstracts;
using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.Entities;
using JobFind.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public bool CreateUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return false;
            }
            var user = new User()
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                Password = userDTO.Password

            };
            _userRepository.Create(user);
            return true;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUser()
        {
            try
            {
                var returnedList = new List<UserDTO>();

                var alluser = await _userRepository.GetAll();
                foreach (var item in alluser)
                {
                    var model = new UserDTO()
                    {
                        UserName = item.UserName,
                        Email = item.Email,
                        Password = item.Password

                    };

                    returnedList.Add(model);
                }

                return returnedList;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion

    }
}
