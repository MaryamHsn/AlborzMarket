﻿using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DomainLayer.DTO
{
    public partial class FileDTO: BaseDTO<int>
    {
        public Guid IdFile { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Subject { get; set; }
        public int Size { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }
        public int? PostId { get; set; }
        public int? EntityEnumId { get; set; }

    }
}
