using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.DTOModels.Response
{
    public class ResponseUpdateCVDTO
    {
        public string Job { get; set; }
        public IList<ResponseEducationDTO> ResponseEducationInformationsDTO { get; set; }
        public IList<ResponseExperienceDTO> ResponseExperienceInformationsDTO { get; set; }
        public string TotalWorkTime { get; set; }
    }
}
