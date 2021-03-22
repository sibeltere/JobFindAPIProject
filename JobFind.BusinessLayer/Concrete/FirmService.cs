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
        private readonly IMapper _mapper;
        #endregion

        #region CTOR
        public FirmService(IMongoRepositoryBase<Firm> firmRepository, IMapper mapper)
        {
            this._firmRepository = firmRepository;
            this._mapper = mapper;
        }
        #endregion

        #region Methods
        public bool CreateFirm(FirmDTO firmDTO)
        {
            if (firmDTO == null)
            {
                return false;
            }
            var firm = _mapper.Map<Firm>(firmDTO);
            _firmRepository.Create(firm);
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
        #endregion

    }
}
