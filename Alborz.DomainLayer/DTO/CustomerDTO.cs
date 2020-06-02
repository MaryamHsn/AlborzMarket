namespace Alborz.DomainLayer.DTO
{
    using Alborz.DomainLayer.DTO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CustomerDTO: BaseDTO<int>
    {
        public int UserId { get; set; }
        public int? CustomerTypeId { get; set; }
        public string CustomerCode { get; set; }
        public bool? IsMemberOfNewsLetter { get; set; }
        public DateTime? LastVisit { get; set; }
        public string ReserveJsonData { get; set; }
    }
}
