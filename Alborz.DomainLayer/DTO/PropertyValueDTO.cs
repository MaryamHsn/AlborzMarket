namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PropertyValueDTO : BaseDTO<int>
    {
        public string Value { get; set; }
        public int? PropertyId { get; set; }
        public IEnumerable<PropertyDTO> Properties{ get; set; }
    }
}
