using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DomainLayer.Entities
{
    [Table("ColorTbl")]
    public partial class ColorTbl : BaseEntity<int>
    {
        [StringLength(50)]
        public string Title { get; set; }
    }
}
