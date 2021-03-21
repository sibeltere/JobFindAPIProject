using JobFind.BusinessLayer.Abstracts;
using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.DTOModels.Request;
using JobFind.DataLayer.DTOModels.Response;
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

        public async Task<IEnumerable<ResponseUserDTO>> GetAllUser()
        {
            try
            {
                var returnedList = new List<ResponseUserDTO>();

                var alluser = await _userRepository.GetAll();
                foreach (var item in alluser)
                {
                    var model = new ResponseUserDTO()
                    {
                        Id = item.Id,
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

        public ResponseUserDTO GetUserByEmail(string email)
        {
            var userDTO = new ResponseUserDTO();
            if (!string.IsNullOrEmpty(email))
            {
                var response = _userRepository.GetFilter(x => x.Email == email);
                if (response != null)
                {
                    userDTO.Id = response.Result.Id;
                    userDTO.Email = response.Result.Email;
                    userDTO.UserName = response.Result.UserName;
                    userDTO.Password = response.Result.Password;

                }
            }

            return userDTO;
        }
        #endregion

    }
}
