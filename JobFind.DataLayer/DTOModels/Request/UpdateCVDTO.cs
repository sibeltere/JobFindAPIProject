using JobFind.DataLayer.DTOModels.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.DTOModels.Request
{
    public class UpdateCVDTO
    {
        public string UserId { get; set; }
        public string Job { get; set; }
        public IList<ResponseEducationDTO> EducationInformationsDTO { get; set; }
        public IList<ResponseExperienceDTO> ExperienceInformationsDTO { get; set; }
    }
}
