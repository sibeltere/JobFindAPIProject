using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.DTOModels.Response
{
    public class ResponseJobPostDTO
    {
        public string Id { get; set; }
        public string FirmId { get; set; }
        public string Definition { get; set; }
        public string Location { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
