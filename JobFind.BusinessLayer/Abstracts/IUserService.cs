using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.BusinessLayer.Abstracts
{
    public interface IUserService
    {
        bool AddUser(UserDTO user);
    }
}
