namespace Alborz.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CartTbl")]
    public partial class CartTbl : BaseEntity<int>
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public CartTbl()
        //{
        //    CartItemTbls = new HashSet<CartItemTbl>();
        //    InvoiceTbls = new HashSet<InvoiceTbl>();
        //} 
        public int? SaleType { get; set; }

        [StringLength(15)]
        public string IP { get; set; }
        public int? UserId { get; set; }
        public int CustomerUserId { get; set; }
        public string CookieToken { get; set; }
        [Required]
        public string Guid { get; set; }
        public DateTime ValidEndDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public bool? IsCanceled { get; set; }
        public int? CancelReasonErrorID { get; set; }
        public virtual ICollection<CartItemTbl> CartItemTbls { get; set; }
        public virtual ErrorTbl ErrorTbl { get; set; }
        public virtual ICollection<InvoiceTbl> InvoiceTbls { get; set; }
    }
}
