namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PropertyDTO : BaseDTO<int>
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public int productId { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<CategoryDTO> Categories{ get; set; }
        public IEnumerable<ProductDTO> Products{ get; set; }
    }
}
