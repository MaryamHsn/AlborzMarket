using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DomainLayer.DTO
{
    public partial class PostDTO : BaseDTO<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
    }
}
