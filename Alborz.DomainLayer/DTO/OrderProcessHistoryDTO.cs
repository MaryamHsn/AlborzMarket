namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderProcessHistoryDTO : BaseDTO<int>
    { 
        public int OrderId { get; set; }
        public int OrderProcessId { get; set; } 
    }
}
