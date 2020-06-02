namespace Alborz.DomainLayer.DTO
{
    using Alborz.DomainLayer.DTO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ErrorDTO : BaseDTO<int>
    {
        [Required]
        public string Title { get; set; }
        public string Code { get; set; }
        public string Descrition { get; set; } 
    }
}
