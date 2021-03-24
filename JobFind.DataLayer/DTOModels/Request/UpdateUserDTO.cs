using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.DTOModels.Request
{
    public class UpdateUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
