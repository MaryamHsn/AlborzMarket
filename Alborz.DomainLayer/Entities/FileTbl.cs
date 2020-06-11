using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DomainLayer.Entities
{
    [Table("FileTbl")]
    public partial class FileTbl : BaseEntity<int>
    {
        public Guid IdFile { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Subject { get; set; }
        public int Size { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int FileTypeEnum { get; set; }
        public int EntityEnumId { get; set; }
        public int EntityKeyId { get; set; }

    }
}
