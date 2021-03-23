using JobFind.DataLayer.DTOModels.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.DTOModels.Response
{
    public class ResponseFirmDTO
    {
        public string Id { get; set; }
        public string FirmName { get; set; }
        public string Address { get; set; }
        public IList<ResponseJobPostDTO> ResponseJobPostDTOs { get; set; }
    }
}
