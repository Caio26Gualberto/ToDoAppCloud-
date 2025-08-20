namespace ToDoApp.Api.DTOs.Api
{
    public class Pagination
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 10;

        public string? SearchTerm = string.Empty;
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
