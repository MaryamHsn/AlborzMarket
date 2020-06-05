namespace Alborz.DomainLayer.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CategoryDTO : BaseDTO<int>
    { 
        [Required]
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public string StartDateString { get; set; }
        public DateTime? EndDate { get; set; }
        public string EndDateString { get; set; }
        public string Code { get; set; }
        public int? priority { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Description { get; set; }
        public string SortOrder { get; set; }
        public string CurrentFilter { get; set; }
        public string SearchString { get; set; }
        public int? Page { get; set; }
        public IEnumerable<CategoryDTO> Categories{ get; set; }
        public PagedList.IPagedList<CategoryDTO> CategoryPageList { get; set; }

    }
}
