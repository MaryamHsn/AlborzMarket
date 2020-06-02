namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductDTO : BaseDTO<int>
    { 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int IsBuyable { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; } 
    }
}
