using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobFind.DataLayer.Entities
{
    public class CV 
    {
        public string Job { get; set; }
        public IList<Education> EducationInformations { get; set; }
        public IList<Experience> ExperienceInformations { get; set; }
    }
}
