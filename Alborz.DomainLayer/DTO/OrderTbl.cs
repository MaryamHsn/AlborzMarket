namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderTbl")]
    public partial class OrderTbl : BaseEntity<int>
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public OrderTbl()
        //{
        //    OrderProcessHistoryTbls = new HashSet<OrderProcessHistoryTbl>();
        //} 
        public int InvoiceId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        [StringLength(50)]
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
        public virtual ErrorTbl ErrorTbl { get; set; }
        public virtual InvoiceTbl InvoiceTbl { get; set; }
        public virtual ICollection<OrderProcessHistoryTbl> OrderProcessHistoryTbls { get; set; }
        public virtual OrderProcessTbl OrderProcessTbl { get; set; }
    }
}
