﻿using System.ComponentModel.DataAnnotations;

namespace AlborzMarket.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "پر کردن مقدار اجباری است")]
        [Display(Name = "دسترسی")]
        public string Name { get; set; }
    }
}