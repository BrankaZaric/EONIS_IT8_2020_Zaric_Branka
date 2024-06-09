using VPDecijeIgracke.Models.ProizvodModel;

namespace VPDecijeIgracke.Data.Specifications
{
    public class ProductSpecParam
    {
        private const int MaxPageSize = 30;
        public int PageIndex { get; set; }
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize; 
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Kategorija { get; set; }
        public string? Naziv {  get; set; }

        private string _search;
        public string? Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
