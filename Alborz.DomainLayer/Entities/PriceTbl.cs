namespace Alborz.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceTbl")]
    public partial class PriceTbl : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public int ProductDetailId { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public bool IsValid { get; set; }
    }
}
