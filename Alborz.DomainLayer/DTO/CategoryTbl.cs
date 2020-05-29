namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryTbl")]
    public partial class CategoryTbl : BaseEntity<int>
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public CategoryTbl()
        //{
        //    CategoryTbl1 = new HashSet<CategoryTbl>();
        //    ProductTbls = new HashSet<ProductTbl>();
        //    PropertyTbls = new HashSet<PropertyTbl>();
        //}
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [StringLength(30)]
        public string Code { get; set; }
        public int? priority { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CategoryTbl> CategoryTbl1 { get; set; }
        public virtual CategoryTbl CategoryTbl2 { get; set; }
        public virtual ICollection<ProductTbl> ProductTbls { get; set; }
        public virtual ICollection<PropertyTbl> PropertyTbls { get; set; }
    }
}
