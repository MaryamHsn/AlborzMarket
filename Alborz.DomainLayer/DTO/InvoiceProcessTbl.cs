namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvoiceProcessTbl")]
    public partial class InvoiceProcessTbl : BaseEntity<int>
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public InvoiceProcessTbl()
        //{
        //    InvoiceProcessHistoryTbls = new HashSet<InvoiceProcessHistoryTbl>();
        //    InvoiceTbls = new HashSet<InvoiceTbl>();
        //}
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        public string Description { get; set; }
        public virtual ICollection<InvoiceProcessHistoryTbl> InvoiceProcessHistoryTbls { get; set; }
        public virtual ICollection<InvoiceTbl> InvoiceTbls { get; set; }
    }
}
