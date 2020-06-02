namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDTO : BaseDTO<int>
    { 
        public int InvoiceId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string OrderDetailNumber { get; set; }
        [Required]
        public string Guid { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsShipped { get; set; }
        public bool IsCancelled { get; set; }
        public int? CancelReasonErrorID { get; set; }
        public int OrderStatusId { get; set; }
        public int CurrentOrderProcessId { get; set; }
        public int AddressId { get; set; }
        public int customrId { get; set; } 
    }
}
