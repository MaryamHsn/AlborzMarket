using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlborzMarket.ViewModel
{
    public class CategoryViewModel
    {
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        [RegularExpression(@"^$|^((1[34][0-9][0-9] |[0-9][0-9])(\/|\-)([0 ۰]{0,1}[۱-۶ 1-6])(\/|\-)([0 ۰]{0,1}[۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}(\/|\-)([۰0]{0,1}[۷-۹ 7-9]|[1۱][۰۱۲012])(\/|\-)([۰0]{0,1}[1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.فرمت صحیح به صورت سال/ماه/روز است")]
        public string StartDateString{ get; set; }
        public DateTime? EndDate { get; set; }
        [RegularExpression(@"^$|^((1[34][0-9][0-9] |[0-9][0-9])(\/|\-)([0 ۰]{0,1}[۱-۶ 1-6])(\/|\-)([0 ۰]{0,1}[۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}(\/|\-)([۰0]{0,1}[۷-۹ 7-9]|[1۱][۰۱۲012])(\/|\-)([۰0]{0,1}[1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$", ErrorMessage = "تاریخ وارد شده نامعتبر است.فرمت صحیح به صورت سال/ماه/روز است")]
        public string EndDateString { get; set; }
        public string Code { get; set; }
        public int? priority { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Description { get; set; }
    }
}