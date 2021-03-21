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
        private readonly IMongoRepositoryBase<CV> _cvRepository;
        #endregion

        #region CTOR
        public CVService(IMongoRepositoryBase<CV> cvRepository)
        {
            this._cvRepository = cvRepository;
        }
        #endregion

        #region Methods
        public bool CreateCV(CVDTO cvDTO)
        {
            if (cvDTO == null)
            {
                return false;
            }
            var cv = new CV()
            {
               


            };
            _cvRepository.Create(cv);
            return true;
        }
        #endregion

    }
}
