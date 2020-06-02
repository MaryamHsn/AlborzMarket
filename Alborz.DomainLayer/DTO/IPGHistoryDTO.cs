namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IPGHistoryDTO: BaseDTO<int>
    { 
        public int? IPGId { get; set; }
        public DateTime? ActiveFromDate { get; set; }
        public DateTime? ActiveToDate { get; set; }
        public DateTime? LastChangeActiveDate { get; set; }
        public string LastChangedUser { get; set; }
    }
}
