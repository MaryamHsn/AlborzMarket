namespace Alborz.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductTbl")]
    public partial class ProductTbl : BaseEntity<int>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int IsBuyable { get; set; }
        public int Quantity { get; set; }
        [StringLength(30)]
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public int CategoryId { get; set; }
        [StringLength(350)]
        public string Title { get; set; }
        public string Color { get; set; }
        [StringLength(50)]
        public string Brand { get; set; }
        public virtual CategoryTbl CategoryTbl { get; set; } 
        public virtual ICollection<PropertyTbl> PropertyTbls { get; set; }
        public virtual ICollection<ProductDetailTbl> ProductDetailTbls { get; set; }

    }
}
