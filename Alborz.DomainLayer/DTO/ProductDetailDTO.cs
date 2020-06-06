using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DomainLayer.DTO
{
    public partial class ProductDetailDTO: BaseDTO<int>
    {
        public int? ColorId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public string ProductTitle { get; set; }
        public string ProductBrand { get; set; }
        public IEnumerable<ColorDTO> Colors{ get; set; }

    }
}
