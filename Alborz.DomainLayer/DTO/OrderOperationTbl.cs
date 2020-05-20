namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderOperation")]
    public partial class OrderOperationTbl : BaseEntity<int>
    { 
        [Required]
        [StringLength(50)]
        public string Number { get; set; }
        public bool IsApproved { get; set; }
        [StringLength(60)]
        public string Status { get; set; }
        public string Comment { get; set; }
        [StringLength(50)]
        public string Currency { get; set; }
        public decimal Sum { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancelledDate { get; set; }
        public string CancelReason { get; set; }
    }
}
