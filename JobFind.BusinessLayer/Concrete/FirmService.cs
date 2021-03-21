using JobFind.BusinessLayer.Abstracts;
using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.Entities;
using JobFind.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.BusinessLayer.Concrete
{
    public class FirmService : IFirmService
    {
        #region Fields
        private readonly IMongoRepositoryBase<Firm> _firmRepository;
        #endregion

        #region CTOR
        public FirmService(IMongoRepositoryBase<Firm> firmRepository)
        {
            this._firmRepository = firmRepository;
        }
        #endregion

        #region Methods
        public bool CreateFirm(FirmDTO firmDTO)
        {
            if (firmDTO == null)
            {
                return false;
            }
            var firm = new Firm()
            {
                FirmName = firmDTO.FirmName,
                Address = firmDTO.Address
            };

            _firmRepository.Create(firm);
            return true;
        }
        #endregion

    }
}
