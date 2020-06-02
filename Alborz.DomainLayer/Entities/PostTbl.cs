using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DomainLayer.Entities
{
    [Table("PostTbl")]
    public partial class PostTbl : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
    }
}
