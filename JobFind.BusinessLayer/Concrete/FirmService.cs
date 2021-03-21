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

        public async Task<IEnumerable<ResponseFirmDTO>> GetAllFirm()
        {
            try
            {
                var returnedList = new List<ResponseFirmDTO>();

                var allfirm = await _firmRepository.GetAll();
                foreach (var item in allfirm)
                {
                    var model = new ResponseFirmDTO()
                    {
                        Id=item.Id,
                       Address=item.Address,
                       FirmName=item.Address
                    };

                    returnedList.Add(model);
                }

                return returnedList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

    }
}
