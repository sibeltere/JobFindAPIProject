using JobFind.DataLayer.DTOModels.Request;
using JobFind.DataLayer.DTOModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobFind.BusinessLayer.Abstracts
{
    public interface IJobPostService
    {
        List<ResponseJobPostDTO> GetJobPostByFirmId(string firmId);
        bool AnyApplyUser(ApplyJobPostDTO applyJobPostDTO);
        ResponseJobPostDTO ApplyJobPost(ApplyJobPostDTO applyJobPostDTO);
        ResponseJobPostDTO GetJobPostById(string jobPostId);
        Task<IEnumerable<ResponseJobPostDTO>> GetAllApplyByJobPost(string jobPostId);
    }
}
