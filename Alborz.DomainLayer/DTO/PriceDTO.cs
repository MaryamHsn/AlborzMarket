namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PriceDTO : BaseDTO<int>
    { 
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public DateTime? ValidDateFrom { get; set; }
        public DateTime? ValidDateTo { get; set; }
        public bool IsVAlid { get; set; }
        public int Priority { get; set; }
    }
}
