using JobFind.DataLayer.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.BusinessLayer.Abstracts
{
    public interface IFirmService
    {
        bool CreateFirm(FirmDTO firmDTO);
    }
}
