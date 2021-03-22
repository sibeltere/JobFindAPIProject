using AutoMapper;
using JobFind.BusinessLayer.Abstracts;
using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.DTOModels.Request;
using JobFind.DataLayer.DTOModels.Response;
using JobFind.DataLayer.Entities;
using JobFind.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFind.BusinessLayer.Concrete
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IMongoRepositoryBase<User> _userRepository;
        private readonly IMapper _mapper;
        #endregion

        #region CTOR
        public UserService(IMongoRepositoryBase<User> userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }
        #endregion

        #region Methods
        public bool CreateUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return false;
            }
            var user = _mapper.Map<User>(userDTO);
            _userRepository.Create(user);
            return true;
        }

        public async Task<IEnumerable<ResponseUserDTO>> GetAllUser()
        {
            var returnedList = new List<ResponseUserDTO>();

            var alluser = await _userRepository.GetAll();
            if (alluser != null)
            {
                returnedList = _mapper.Map<List<ResponseUserDTO>>(alluser);
            }
            return returnedList;
        }

        public ResponseUserDTO GetUserByEmail(string email)
        {
            var userDTO = new ResponseUserDTO();
            if (!string.IsNullOrEmpty(email))
            {
                var response = _userRepository.GetFilter(x => x.Email == email);
                if (response.Result != null)
                {
                    userDTO = _mapper.Map<ResponseUserDTO>(response.Result);
                }
            }

            return userDTO;
        }
        #endregion

    }
}
