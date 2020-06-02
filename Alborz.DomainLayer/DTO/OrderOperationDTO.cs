namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderOperationDTO : BaseDTO<int>
    { 
        [Required]
        public string Number { get; set; }
        public bool IsApproved { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public string Currency { get; set; }
        public decimal Sum { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancelledDate { get; set; }
        public string CancelReason { get; set; }
    }
}
