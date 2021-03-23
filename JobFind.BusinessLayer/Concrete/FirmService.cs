using AutoMapper;
using JobFind.BusinessLayer.Abstracts;
using JobFind.DataLayer.DTOModels;
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
    public class FirmService : IFirmService
    {
        #region Fields
        private readonly IMongoRepositoryBase<Firm> _firmRepository;
        private readonly IMongoRepositoryBase<JobPost> _jobPostRepository;
        private readonly IMapper _mapper;
        #endregion

        #region CTOR
        public FirmService(IMongoRepositoryBase<Firm> firmRepository, IMongoRepositoryBase<JobPost> jobPostRepository,IMapper mapper)
        {
            this._firmRepository = firmRepository;
            this._jobPostRepository = jobPostRepository;
            this._mapper = mapper;
        }
        #endregion

        #region Methods
        public bool CreateFirm(FirmDTO firmDTO)
        {
            if (firmDTO == null)
                return false;

            var firm = _mapper.Map<Firm>(firmDTO);
            _firmRepository.Create(firm);
            return true;
        }

        public bool DeleteFirm(string firmId)
        {
            //firma silinirken firmanın yayında ilanı olup olmadığı bakılmalı eğer varsa ilan silinip sonra firma silinmeli
            if (string.IsNullOrEmpty(firmId))
                return false;

            var jobPostList = _jobPostRepository.GetAll(x => x.FirmId == firmId).Result;
            if (jobPostList != null)
            {
                foreach (var item in jobPostList)
                {
                    _jobPostRepository.Delete(item.Id);
                }
            }
            _firmRepository.Delete(firmId);
            return true;
        }

        public async Task<IEnumerable<ResponseFirmDTO>> GetAllFirm()
        {
            var returnedList = new List<ResponseFirmDTO>();
            
            var allfirm = await _firmRepository.GetAll();
            if (allfirm != null)
            {
                returnedList = _mapper.Map<List<ResponseFirmDTO>>(allfirm);
            }
            return returnedList;
        }

        public ResponseFirmDTO GetFirmById(string Id)
        {
            var firmDTO = new ResponseFirmDTO();
            if (!string.IsNullOrEmpty(Id))
            {
                var response = _firmRepository.GetFilter(x => x.Id == Id);
                if (response.Result != null)
                {
                    firmDTO = _mapper.Map<ResponseFirmDTO>(response.Result);
                }
            }

            return firmDTO;
        }

        public bool AddFirmJobPost(JobPostDTO jobPostDTO)
        {
            if (jobPostDTO == null)
                return false;

            var jobPost = _mapper.Map<JobPost>(jobPostDTO);
            _jobPostRepository.Create(jobPost);

            var firm = _firmRepository.Find(jobPostDTO.FirmId);

            firm.Result.JobPosts.Add(jobPost);
            _firmRepository.Update(firm.Result);
            return true;
        }
        #endregion

    }
}
