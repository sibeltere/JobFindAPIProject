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
        bool DeleteFirm(string firmId);
        bool AddFirmJobPost(JobPostDTO jobPostDTO);
        Task<IEnumerable<ResponseFirmDTO>> GetAllFirm();
        ResponseFirmDTO GetFirmById(string Id);
    }
}
