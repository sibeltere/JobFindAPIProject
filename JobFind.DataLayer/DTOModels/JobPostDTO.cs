using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.DTOModels
{
    public class JobPostDTO
    {
        public string Definition { get; set; }
        public string Location { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
