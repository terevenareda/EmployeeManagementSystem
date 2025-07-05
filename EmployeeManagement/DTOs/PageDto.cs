namespace EmployeeManagement.DTOs
{
    public class PageDto<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }

        public PageDto(List<T> data, int count, int pageNumber, int pageSize)
        {
            Data = data;
            TotalCount = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
