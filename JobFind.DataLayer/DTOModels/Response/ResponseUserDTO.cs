using JobFind.DataLayer.DTOModels.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.DTOModels.Response
{
    public class ResponseUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ResponseCVDTO ResponseCVDTO { get; set; }
    }
}
