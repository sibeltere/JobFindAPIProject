using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobFind.DataLayer.Entities
{
    [Table("Firm")]
    public class Firm : BaseEntity
    {
        public string FirmName { get; set; }
        public string Address { get; set; }
        public IList<JobPost> JobPosts { get; set; }
        public Firm()
        {
            JobPosts = new List<JobPost>();
        }
    }
}
