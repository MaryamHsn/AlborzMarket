using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DomainLayer.DTO
{
    public partial class ProductDetailDTO: BaseDTO<int>
    {
        public int? ColorId { get; set; }
        public string ColorName { get; set; }
        public int? ProductId { get; set; }
    //    [Required(ErrorMessage = "پر کردن این مقدار اجباری است")]
        public int Quantity { get; set; }
        public string ProductTitle { get; set; }
        public string ProductBrand { get; set; }
        public string SortOrder { get; set; }
        public string CurrentFilter { get; set; } 
        public int? Page { get; set; }
        public ProductDTO Product{ get; set; }
        public List<ProductDetailDTO> ProductDetails { get; set; }
        public IEnumerable<ColorDTO> Colors { get; set; }
        public IEnumerable<PropertyDTO> Properties { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
        //public IEnumerable<ProductDTO> Products{ get; set; }
        public PagedList.IPagedList<ProductDetailDTO> ProductDetailsPageList { get; set; } 

    }
}
