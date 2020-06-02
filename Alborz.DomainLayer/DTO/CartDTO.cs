namespace Alborz.DomainLayer.DTO
{ 
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class CartDTO: BaseDTO<int>
    {
        public int? SaleType { get; set; }
        public string IP { get; set; }
        public int? UserId { get; set; }
        public int CustomerUserId { get; set; }
        public string CookieToken { get; set; }
        [Required]
        public string Guid { get; set; }
        public DateTime ValidEndDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public bool? IsCanceled { get; set; }
        public int? CancelReasonErrorID { get; set; } 
    }
}
