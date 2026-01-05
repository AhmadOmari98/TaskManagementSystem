
namespace TaskManagementSystem.Application.DTOs
{
    public class SearchPageDto<T>
    {
        public int Page { get; set; } = 1;
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public T Criteria { get; set; }
    }
}
