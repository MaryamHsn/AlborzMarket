namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderProcessHistoryTbl")]
    public partial class OrderProcessHistoryTbl : BaseEntity<int>
    { 
        public int OrderId { get; set; }
        public int OrderProcessId { get; set; }
        public virtual OrderProcessTbl OrderProcessTbl { get; set; }
        public virtual OrderTbl OrderTbl { get; set; }
    }
}
