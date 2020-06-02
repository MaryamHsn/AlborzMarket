namespace Alborz.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvoiceTbl")]
    public partial class InvoiceTbl : BaseEntity<int>
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public InvoiceTbl()
        //{
        //    InvoiceProcessHistoryTbls = new HashSet<InvoiceProcessHistoryTbl>();
        //    OrderTbls = new HashSet<OrderTbl>();
        //} 
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        [StringLength(50)]
        public string InvoiceCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        [StringLength(50)]
        public string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal FinalPrice { get; set; }
        [StringLength(15)]
        public string IP { get; set; }
        public int? CurrentProcessId { get; set; }
        public int? PaymentTypeId { get; set; }
        public bool? IsPayed { get; set; }
        public virtual CartTbl CartTbl { get; set; }
        public virtual ICollection<InvoiceProcessHistoryTbl> InvoiceProcessHistoryTbls { get; set; }
        public virtual InvoiceProcessTbl InvoiceProcessTbl { get; set; }
        public virtual ICollection<OrderTbl> OrderTbls { get; set; }
    }
}
