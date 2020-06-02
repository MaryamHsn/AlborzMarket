namespace Alborz.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderProcessTbl")]
    public partial class OrderProcessTbl : BaseEntity<int>
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public OrderProcessTbl()
        //{
        //    OrderProcessHistoryTbls = new HashSet<OrderProcessHistoryTbl>();
        //    OrderTbls = new HashSet<OrderTbl>();
        //} 
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        public string Description { get; set; }
        public virtual ICollection<OrderProcessHistoryTbl> OrderProcessHistoryTbls { get; set; }
        public virtual ICollection<OrderTbl> OrderTbls { get; set; }
    }
}
