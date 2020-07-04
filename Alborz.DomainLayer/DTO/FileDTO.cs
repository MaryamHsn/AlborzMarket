using Alborz.DomainLayer.DTO;
using Alborz.DomainLayer.Enumration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Alborz.DomainLayer.DTO
{
    public partial class FileDTO: BaseDTO<int>
    {
        public Guid IdFile { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Subject { get; set; }
        public int Size { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileTypeEnum FileTypeEnum { get; set; }
        public int EntityEnumId { get; set; }
        public int EntityKeyId { get; set; }
        public int ProductId { get; set; }
        public HttpPostedFileBase File { get; set; }
        public List<HttpPostedFileBase> Files { get; set; }
    }
}
