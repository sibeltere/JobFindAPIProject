using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobFind.DataLayer.Entities
{
    [Table("JobPost")]
    public class JobPost : BaseEntity
    {
        public string FirmId { get; set; }
        public string Definition { get; set; }
        public string Location { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<string> ApplyUsers { get; set; }
        public JobPost()
        {
            ApplyUsers = new List<string>();
        }
    }
}
