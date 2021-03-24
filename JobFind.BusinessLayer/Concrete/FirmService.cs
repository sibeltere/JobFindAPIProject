using AutoMapper;
using JobFind.BusinessLayer.Abstracts;
using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.DTOModels.Request;
using JobFind.DataLayer.DTOModels.Response;
using JobFind.DataLayer.Entities;
using JobFind.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public FirmService(IMongoRepositoryBase<Firm> firmRepository, IMongoRepositoryBase<JobPost> jobPostRepository, IMapper mapper)
        {
            this._firmRepository = firmRepository;
            this._jobPostRepository = jobPostRepository;
            this._mapper = mapper;
        }
        #endregion

        #region Methods
        public ResponseFirmDTO CreateFirm(FirmDTO model)
        {
            var firmDTO = new ResponseFirmDTO();
            if (model != null)
            {
                var mapperFirm = _mapper.Map<Firm>(model);
                var addedFirm = _firmRepository.Create(mapperFirm);
                firmDTO = _mapper.Map<ResponseFirmDTO>(addedFirm.Result);
            }
            return firmDTO;
        }

        public bool DeleteFirm(string firmId)
        {
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

        public ResponseJobPostDTO AddFirmJobPost(JobPostDTO jobPostDTO)
        {
            var responseJobPostDTO = new ResponseJobPostDTO();
            if (jobPostDTO == null)
                return null;

            var jobPost = _mapper.Map<JobPost>(jobPostDTO);
            _jobPostRepository.Create(jobPost);

            var firm = _firmRepository.Find(jobPostDTO.FirmId);

            firm.Result.JobPosts.Add(jobPost);
            _firmRepository.Update(firm.Result);

            responseJobPostDTO = _mapper.Map<ResponseJobPostDTO>(jobPost);

            return responseJobPostDTO;
        }

        public ResponseFirmDTO UpdateFirm(UpdateFirmDTO updateFirmDTO)
        {
            if (updateFirmDTO == null)
                return null;

            var responseFirmDTO = new ResponseFirmDTO();
            var updatedFirm = _mapper.Map<Firm>(updateFirmDTO);
            _firmRepository.Update(updatedFirm);

            responseFirmDTO = _mapper.Map<ResponseFirmDTO>(updatedFirm);
            return responseFirmDTO;
        }

        public ResponseJobPostDTO UpdateFirmJobPost(UpdateJobPostDTO updateJobPostDTO)
        {
            if (updateJobPostDTO == null)
                return null;

            var responseJobPostDTO = new ResponseJobPostDTO();

            var jobPost = _mapper.Map<JobPost>(updateJobPostDTO);
            _jobPostRepository.Update(jobPost);


            //firmanında ilgli ilanı güncellenir.
            var firm = _firmRepository.Find(updateJobPostDTO.FirmId);

            foreach (var item in firm.Result.JobPosts)
            {
                if (item.Id == jobPost.Id)
                {
                    item.Definition = jobPost.Definition;
                    item.Location = jobPost.Location;
                    item.ExpirationDate = jobPost.ExpirationDate;
                }
            }

            _firmRepository.Update(firm.Result);
            responseJobPostDTO = _mapper.Map<ResponseJobPostDTO>(jobPost);
            return responseJobPostDTO;
        }
        #endregion

    }
}
