namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PaymentTbl")]
    public partial class PaymentTbl : BaseEntity<int>
    { 
        public int InvoiceId { get; set; }
        [StringLength(250)]
        public string BankCode { get; set; }
        [StringLength(15)]
        public string IP { get; set; }
        public string PublicToken { get; set; }
        public string SecurityToken { get; set; }
        public int ResponseShoppingCode { get; set; }
        public string ResponseShoppingDescription { get; set; }
        public string ReservationCodeBeforePay { get; set; }
        public DateTime ReservationCodeBeforePayDate { get; set; }
        public string SoldProductJson { get; set; }
        public int ValidTPay { get; set; }
        public string ValidTPaydescription { get; set; }
        public string ReturnLink { get; set; }
        public string SendLink { get; set; }
        public DateTime IssuanceDate { get; set; }
        public string ReservationCodeForTransaction { get; set; }
        public string PaymentStatus { get; set; }
        public bool SalesReservedVerify { get; set; }
        public bool SendPaymentResultWebservice { get; set; }
        public int ResponsePayCode { get; set; }
        public DateTime ResponsePayDate { get; set; }
        public int RefrenceId { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int WaitSecond { get; set; }
        public int FollowUpId { get; set; }
        public decimal Amount { get; set; }
        public bool IsSuccessful { get; set; }
        public string FailedReason { get; set; }
        public string GatewayCode { get; set; }
    }
}
