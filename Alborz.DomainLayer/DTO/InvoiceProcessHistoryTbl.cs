namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvoiceProcessHistoryTbl")]
    public partial class InvoiceProcessHistoryTbl : BaseEntity<int>
    {
        public int InvoiceId { get; set; }
        public int? InvoiceHistoryStatus { get; set; }
        public int InvoiceProcessId { get; set; }
        public virtual InvoiceProcessTbl InvoiceProcessTbl { get; set; }
        public virtual InvoiceTbl InvoiceTbl { get; set; }
    }
}
