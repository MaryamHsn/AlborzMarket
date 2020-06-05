using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DomainLayer.Enumration
{ 
    public enum ColorEnum
    {
        [Display(Name = "نا مشخص")]
        NotCooler = 0,
        [Display(Name = "آبی")]
        Blue = 1,
        [Display(Name = "سبز")]
        Green = 2,
        [Display(Name = "زرد")]
        Yellow = 3,
        [Display(Name = "قرمز")]
        Red = 4,
        [Display(Name = "سفید")]
        White = 5
    }
}
