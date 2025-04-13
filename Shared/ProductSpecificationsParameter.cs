using System;

namespace Shared
{
    public class ProductSpecificationsParameter
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }

        private int _pageIndex = 1;
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }
        private int _pageSize = 5;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
    }
}
