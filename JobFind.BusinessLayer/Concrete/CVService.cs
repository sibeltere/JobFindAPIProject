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
    public class CVService : ICVService
    {
        #region Fields
        private readonly IMongoRepositoryBase<User> _userRepository;
        private readonly IMapper _mapper;
        #endregion

        #region CTOR
        public CVService(IMongoRepositoryBase<User> userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }
        #endregion

        #region Methods
        public ResponseCVDTO CreateCV(CVDTO cvDTO)
        {
            if (cvDTO == null)
                return null;
            var responseCVDTO = new ResponseCVDTO();

            var cv = _mapper.Map<CV>(cvDTO);
            var user = _userRepository.GetFilter(x => x.Id == cvDTO.UserId);
            user.Result.CV = cv;
            _userRepository.Update(user.Result);

            responseCVDTO = _mapper.Map<ResponseCVDTO>(cv);

            //Toplam çalışma süresini hesaplayı response da gösterilmesi amaçlanmaktadır.
            TimeSpan workTime = new TimeSpan();
            foreach (var experience in responseCVDTO.ResponseExperienceInformationsDTO)
            {
                workTime += experience.EndDate - experience.StartDate;
            }
            var year = (int)(workTime.Days / 365.2425);
            var month = (int)((workTime.Days % 365.2425) / 30.436875);
            var day = (int)(((workTime.Days % 365.2425) % 30.436875));

            responseCVDTO.TotalWorkTime = year + " Yıl " + month + " Ay " + day + " Gün";

            return responseCVDTO;
        }

        public ResponseUpdateCVDTO UpdateCV(UpdateCVDTO updateCVDTO)
        {
            if (updateCVDTO == null)
                return null;

            var responseUpdateCVDTO= new ResponseUpdateCVDTO();

            var cv = _mapper.Map<CV>(updateCVDTO);
            var user = _userRepository.GetFilter(x => x.Id == updateCVDTO.UserId);
            user.Result.CV = cv;
            _userRepository.Update(user.Result);

            responseUpdateCVDTO = _mapper.Map<ResponseUpdateCVDTO>(cv);

            //Toplam çalışma süresini hesaplayı response da gösterilmesi amaçlanmaktadır.
            TimeSpan workTime = new TimeSpan();
            foreach (var experience in responseUpdateCVDTO.ResponseExperienceInformationsDTO)
            {
                workTime += experience.EndDate - experience.StartDate;
            }
            var year = (int)(workTime.Days / 365.2425);
            var month = (int)((workTime.Days % 365.2425) / 30.436875);
            var day = (int)(((workTime.Days % 365.2425) % 30.436875));

            responseUpdateCVDTO.TotalWorkTime = year + " Yıl " + month + " Ay " + day + " Gün";
            return responseUpdateCVDTO;
        }
        #endregion

    }
}
