using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.DTOModels.Request;
using JobFind.DataLayer.DTOModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobFind.BusinessLayer.Abstracts
{
    public interface IFirmService
    {
        bool CreateFirm(FirmDTO firmDTO);
        Task<IEnumerable<ResponseFirmDTO>> GetAllFirm();
    }
}
