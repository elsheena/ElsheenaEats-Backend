using Core.Models;
using Core.Common;

namespace BusinessLogicLayer.DTOs
{
    public class DishQueryParamsDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<Category>? Categories { get; set; }
        public bool? IsVegetarian { get; set; }
        public SortBy SortBy { get; set; } = SortBy.NameAsc;
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
    }
}