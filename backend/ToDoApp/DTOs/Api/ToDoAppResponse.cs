namespace ToDoApp.Api.DTOs.Api
{
    public class ToDoAppResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
