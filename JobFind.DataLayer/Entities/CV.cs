﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobFind.DataLayer.Entities
{
    [Table("CV")]
    public class CV : BaseEntity
    {
        public string Job { get; set; }
        public IList<Education> EducationInformations { get; set; }
        public IList<Experience> ExperienceInformations { get; set; }
        public int TotalWorkTime { get; set; }
    }
}