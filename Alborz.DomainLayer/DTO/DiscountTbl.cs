namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Discount")]
    public partial class DiscountTbl : BaseEntity<int>
    { 
        public int? CartItemId { get; set; }
        public int? CartId { get; set; }
        public int? CategoryId { get; set; }
        public int? CustomerTypeId { get; set; }
        public int? CustomerId { get; set; }
        public int? InvoiceId { get; set; }
        public int? ProductId { get; set; }
        [StringLength(250)]
        public string Title { get; set; }
        public decimal PriceDiscount { get; set; }
        public decimal PerecentDiscount { get; set; }
        public DateTime? StartDateValid { get; set; }
        public DateTime? EndDateValid { get; set; }
    }
}
