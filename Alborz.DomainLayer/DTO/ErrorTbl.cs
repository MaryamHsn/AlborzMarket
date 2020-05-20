namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ErrorTbl")]
    public partial class ErrorTbl : BaseEntity<int>
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public ErrorTbl()
        //{
        //    CartTbls = new HashSet<CartTbl>();
        //    OrderTbls = new HashSet<OrderTbl>();
        //} 
        [Required]
        [StringLength(400)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        public string Descrition { get; set; }
        public virtual ICollection<CartTbl> CartTbls { get; set; }
        public virtual ICollection<OrderTbl> OrderTbls { get; set; }
    }
}
