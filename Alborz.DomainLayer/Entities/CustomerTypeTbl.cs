namespace Alborz.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerTypeTbl")]
    public partial class CustomerTypeTbl : BaseEntity<int>
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public CustomerTypeTbl()
        //{
        //    CustomerTbls = new HashSet<CustomerTbl>();
        //}
        [StringLength(50)]
        public string CustomerCode { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public virtual ICollection<CustomerTbl> CustomerTbls { get; set; }
    }
}
