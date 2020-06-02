namespace Alborz.DomainLayer.DTO
{
    using Alborz.DomainLayer.DTO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InvoiceDTO: BaseDTO<int>
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public string InvoiceCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal FinalPrice { get; set; }
        public string IP { get; set; }
        public int? CurrentProcessId { get; set; }
        public int? PaymentTypeId { get; set; }
        public bool? IsPayed { get; set; }
    }
}
