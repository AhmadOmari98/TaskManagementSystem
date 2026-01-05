
namespace TaskManagementSystem.Application.DTOs
{
    public class PagedDataDto<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
