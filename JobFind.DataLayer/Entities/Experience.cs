using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobFind.DataLayer.Entities
{
    [Table("Experience")]
    public class Experience : BaseEntity
    {
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string PositionDefinition { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
