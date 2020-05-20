namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductTbl")]
    public partial class ProductTbl : BaseEntity<int>
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public ProductTbl()
        //{
        //    ProductTbl1 = new HashSet<ProductTbl>();
        //    PropertyTbls = new HashSet<PropertyTbl>();
        //} 
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
        [StringLength(50)]
        public string Brand { get; set; }
        public virtual CategoryTbl CategoryTbl { get; set; }
        public virtual ICollection<ProductTbl> ProductTbl1 { get; set; }
        public virtual ProductTbl ProductTbl2 { get; set; }
        public virtual ICollection<PropertyTbl> PropertyTbls { get; set; }
    }
}
