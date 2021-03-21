using JobFind.DataLayer.DTOModels.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobFind.BusinessLayer.Abstracts
{
    public interface ICVService
    {
        bool CreateCV(CVDTO cvDTO);
    }
}
