using AutoMapper;
using JobFind.BusinessLayer.Abstracts;
using JobFind.DataLayer.DTOModels.Request;
using JobFind.DataLayer.DTOModels.Response;
using JobFind.DataLayer.Entities;
using JobFind.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobFind.BusinessLayer.Concrete
{
    public class JobPostService : IJobPostService
    {
        #region Fields
        private readonly IMongoRepositoryBase<JobPost> _jobPostRepository;
        private readonly IMapper _mapper;

        #endregion

        #region CTOR
        public JobPostService(IMongoRepositoryBase<JobPost> jobPostRepository, IMapper mapper)
        {
            this._jobPostRepository = jobPostRepository;
            this._mapper = mapper;
        }
        #endregion

        #region Methods
        public bool AnyApplyUser(ApplyJobPostDTO applyJobPostDTO)
        {
            if (applyJobPostDTO==null)
                return false;

            var response = _jobPostRepository.GetFilter(x=>x.Id==applyJobPostDTO.JobPostId 
                          && x.ApplyUsers.Contains(applyJobPostDTO.UserId));

            if (response.Result == null)
                return false;

            return true;
        }
        public List<ResponseJobPostDTO> GetJobPostByFirmId(string firmId)
        {
            var list = new List<ResponseJobPostDTO>();
            if (!string.IsNullOrEmpty(firmId))
            {
                var jobPostList = _jobPostRepository.GetAll(x => x.FirmId == firmId);
                list = _mapper.Map<List<ResponseJobPostDTO>>(jobPostList);
            }
            return list;
        }


        public bool ApplyJobPost(ApplyJobPostDTO applyJobPostDTO)
        {
            if (applyJobPostDTO == null)
                return false;
            var jobPost = _jobPostRepository.GetFilter(x => x.Id == applyJobPostDTO.JobPostId);
            if (jobPost.Result == null)
                return false;
            jobPost.Result.ApplyUsers.Add(applyJobPostDTO.UserId);
            _jobPostRepository.Update(jobPost.Result);
            return true;
        }

        public ResponseJobPostDTO GetJobPostById(string jobPostId)
        {
            var jobPostDTO = new ResponseJobPostDTO();
            if (!string.IsNullOrEmpty(jobPostId))
            {
                var response = _jobPostRepository.GetFilter(x => x.Id == jobPostId);
                if (response.Result != null)
                {
                    jobPostDTO = _mapper.Map<ResponseJobPostDTO>(response.Result);
                }
            }

            return jobPostDTO;
        }

        public async Task<IEnumerable<ResponseJobPostDTO>> GetAllApplyByJobPost(string jobPostId)
        {
            var returnedList = new List<ResponseJobPostDTO>();

            var allJobPost = await _jobPostRepository.GetAll(x => x.Id==jobPostId);
            if (allJobPost != null)
            {
                returnedList = _mapper.Map<List<ResponseJobPostDTO>>(allJobPost);
            }
            return returnedList;
        }
        #endregion

    }
}
