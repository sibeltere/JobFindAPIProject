using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobFind.BusinessLayer.Abstracts
{
    public interface IUserService
    {
        bool CreateUser(UserDTO userDTO);
        Task<IEnumerable<UserDTO>> GetAllUser();
    }
}
