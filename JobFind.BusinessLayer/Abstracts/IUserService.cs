using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.DTOModels.Request;
using JobFind.DataLayer.DTOModels.Response;
using JobFind.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobFind.BusinessLayer.Abstracts
{
    public interface IUserService
    {
        ResponseUserDTO CreateUser(UserDTO userDTO);
        ResponseUserDTO UpdateUser(UpdateUserDTO userDTO);
        bool DeleteUser(string userId);
        Task<IEnumerable<ResponseUserDTO>> GetAllUser();
        ResponseUserDTO GetUserByEmail(string email);
        ResponseUserDTO GetUserById(string Id);
       
    }
}
