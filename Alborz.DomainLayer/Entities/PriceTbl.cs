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
        [StringLength(250)]
        public string ProductTitle { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string Currency { get; set; }
        public DateTime? ValidDateFrom { get; set; }
        public DateTime? ValidDateTo { get; set; }
        public bool IsVAlid { get; set; }
        public int Priority { get; set; }
    }
}
