namespace Alborz.DomainLayer.DTO
{
    using Alborz.DomainLayer.Enumration;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductDTO : BaseDTO<int>
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        //[RegularExpression(@"^$|^((1[34][0-9][0-9] |[0-9][0-9])(\/|\-)([0 ۰]{0,1}[۱-۶ 1-6])(\/|\-)([0 ۰]{0,1}[۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}(\/|\-)([۰0]{0,1}[۷-۹ 7-9]|[1۱][۰۱۲012])(\/|\-)([۰0]{0,1}[1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.فرمت صحیح به صورت سال/ماه/روز است")]
        public string StartDateString { get; set; }
        public DateTime? EndDate { get; set; }
        //[RegularExpression(@"^$|^((1[34][0-9][0-9] |[0-9][0-9])(\/|\-)([0 ۰]{0,1}[۱-۶ 1-6])(\/|\-)([0 ۰]{0,1}[۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}(\/|\-)([۰0]{0,1}[۷-۹ 7-9]|[1۱][۰۱۲012])(\/|\-)([۰0]{0,1}[1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.فرمت صحیح به صورت سال/ماه/روز است")]
        public string EndDateString { get; set; }
        public int IsBuyable { get; set; }
        public int Quantity { get; set; }
        [Required(ErrorMessage ="وارد کردن کد اجباری است")]
        public string Code { get; set; } 
        [Required(ErrorMessage ="پر کردن این مقدار اجباری است")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public int ColorId { get; set; }
        [Required(ErrorMessage = "وارد کردن عنوان اجباری است")]
        public string Title { get; set; }
        [Required(ErrorMessage = "وارد کردن برند اجباری است")]
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SortOrder { get; set; }
        public string CurrentFilter { get; set; }
        public string SearchString { get; set; }
        public int? Page { get; set; }
        public IList<ProductDetailDTO> ProductDetails{ get; set; }
        public IEnumerable<ColorDTO> Colors{ get; set; }
        public IEnumerable<PropertyDTO> Properties{ get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public PagedList.IPagedList<ProductDTO> ProductsPageList { get; set; }
    }
}
