using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DomainLayer.Entities
{
    [Table("ProductDetailTbl")]
    public partial class ProductDetailTbl :BaseEntity<int>
    { 
        public int? ColorId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public virtual ProductTbl ProductTbl { get; set; }
    }
}
