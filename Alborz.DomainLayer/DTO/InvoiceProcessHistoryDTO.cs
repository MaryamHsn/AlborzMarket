namespace Alborz.DomainLayer.DTO
{
    using Alborz.DomainLayer.DTO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InvoiceProcessHistoryDTO: BaseDTO<int>
    {
        public int InvoiceId { get; set; }
        public int? InvoiceHistoryStatus { get; set; }
        public int InvoiceProcessId { get; set; } 
    }
}
