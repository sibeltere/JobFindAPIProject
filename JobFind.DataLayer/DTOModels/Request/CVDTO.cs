using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.DTOModels.Request
{
    public class CVDTO
    {
        public string Job { get; set; }
        public IList<EducationDTO> EducationInformations { get; set; }
        public IList<ExperienceDTO> ExperienceInformations { get; set; }
        
    }
}
