using ECommerceApi.DTOs;

namespace ECommerceApi.Helpers
{
    public class Pagination<T>
    {
       
        public Pagination(int pageSize1, int pageSize2, IReadOnlyList<T> mappedProducts , int count)
        {
            PageSize = pageSize1;
            PageIndex = pageSize2;
            Data = mappedProducts;
            Count = count;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }

    }
}
