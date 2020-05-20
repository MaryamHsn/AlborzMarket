namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PropertyTbl")]
    public partial class PropertyTbl : BaseEntity<int>
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public PropertyTbl()
        //{
        //    PropertyValueTbls = new HashSet<PropertyValueTbl>();
        //} 
        [StringLength(150)]
        public string Title { get; set; }
        public int productId { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryTbl CategoryTbl { get; set; }
        public virtual ProductTbl ProductTbl { get; set; }
        public virtual ICollection<PropertyValueTbl> PropertyValueTbls { get; set; }
    }
}
