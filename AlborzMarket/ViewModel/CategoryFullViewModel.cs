using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlborzMarket.ViewModel
{
    public class CategoryFullViewModel
    {
        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? page { get; set; }
        public PagedList.IPagedList<AlborzMarket.ViewModel.CategoryViewModel> CategoryViewModels { get; set; }

    }
}