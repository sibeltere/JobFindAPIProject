using System;
using System.Collections.Generic;
using System.Text;

namespace JobFind.DataLayer.DTOModels.Request
{
    public class UpdateFirmDTO
    {
        public string Id { get; set; }
        public string FirmName { get; set; }
        public string Address { get; set; }
    }
}
