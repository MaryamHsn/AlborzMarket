namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PropertyValueTbl")]
    public partial class PropertyValueTbl : BaseEntity<int>
    {
        [StringLength(350)] 
        public string Value { get; set; }
        public int? PropertyId { get; set; }
        public virtual PropertyTbl PropertyTbl { get; set; }
    }
}
