using JobFind.DataLayer.DTOModels.Request;
using JobFind.DataLayer.DTOModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobFind.BusinessLayer.Abstracts
{
    public interface ICVService
    {
        ResponseCVDTO CreateCV(CVDTO cvDTO);
        ResponseUpdateCVDTO UpdateCV(UpdateCVDTO updateCVDTO);
    }
}
