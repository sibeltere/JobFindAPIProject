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
        ResponseFirmDTO CreateFirm(FirmDTO firmDTO);
        ResponseFirmDTO UpdateFirm(UpdateFirmDTO updateFirmDTO);
        bool DeleteFirm(string firmId);
        ResponseJobPostDTO AddFirmJobPost(JobPostDTO jobPostDTO);
        ResponseJobPostDTO UpdateFirmJobPost(UpdateJobPostDTO updateJobPostDTO);
        Task<IEnumerable<ResponseFirmDTO>> GetAllFirm();
        ResponseFirmDTO GetFirmById(string Id);
    }
}
