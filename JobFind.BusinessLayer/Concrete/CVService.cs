using AutoMapper;
using JobFind.BusinessLayer.Abstracts;
using JobFind.DataLayer.DTOModels.Request;
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
        public bool CreateCV(CVDTO cvDTO)
        {
            if (cvDTO == null)
            {
                return false;
            }
            var cv = _mapper.Map<CV>(cvDTO);
            var user = _userRepository.GetFilter(x => x.Id == cvDTO.UserId);
            user.Result.CV = cv;
            _userRepository.Update(user.Result);
            return true;
        }
        #endregion

    }
}
