namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CartItemTbl")]
    public partial class CartItemTbl : BaseEntity<int>
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddToBasketDateTime { get; set; }
        [Required]
        public string Guid { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
        public bool? IsCanceled { get; set; }
        public string ReserveJsonData { get; set; }
        public virtual CartTbl CartTbl { get; set; }
    }
}
