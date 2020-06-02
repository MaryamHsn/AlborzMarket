using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DomainLayer.DTO
{
    public  class CustomerTypeDTO : BaseDTO<int>
    { 
        public string CustomerCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
