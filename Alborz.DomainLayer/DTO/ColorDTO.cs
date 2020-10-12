using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DomainLayer.DTO
{
    public partial class ColorDTO :BaseDTO<int>
    { 
        public string Title { get; set; }
        public string SortOrder { get; set; }
        public string CurrentFilter { get; set; }
        public string SearchString { get; set; }
        public int? Page { get; set; }
        //public IEnumerable<CategoryDTO> Categories { get; set; }
        public PagedList.IPagedList<ColorDTO> ColorPageList { get; set; }
    }
}
