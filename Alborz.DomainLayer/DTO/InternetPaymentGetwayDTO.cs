namespace Alborz.DomainLayer.DTO
{
    using Alborz.DomainLayer.DTO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InternetPaymentGetwayDTO : BaseDTO<int>
    { 
        public string BankTitle { get; set; }
        public string BankLogo { get; set; }
        public string IPGCode { get; set; }
        public bool? IsOnSite { get; set; }
        public string ExtraCode { get; set; }
        public string IPGDescription { get; set; }
        public string BankPublicToken { get; set; }
        public string IPGUrl { get; set; }
        public string ReserveJsonData { get; set; }
    }
}
