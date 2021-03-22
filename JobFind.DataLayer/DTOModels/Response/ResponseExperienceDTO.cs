using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.DTOModels.Response
{
    public class ResponseExperienceDTO
    {
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string PositionDefinition { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
