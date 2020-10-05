using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DomainLayer.DTO
{
    public class HomeDTO
    {
        public List<ProductDTO> Newest { get; set; }
        public List<ProductDTO> MostSale { get; set; }
    }
}
