namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PriceDTO : BaseDTO<int>
    { 
        public int ProductDetailId { get; set; } 
        public int ProductId { get; set; } 
        public int? Quantity { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; } 
        public bool IsValid { get; set; } 
    }
}
