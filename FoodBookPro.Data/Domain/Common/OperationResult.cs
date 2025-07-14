namespace FoodBookPro.Data.Domain.Common
{
    public class OperationResult<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public bool IsSuccess { get; set; }

        private OperationResult(T? data, string? message, bool isSuccess, List<string>? errors)
        {
            Data = data;
            Message = message;
            Errors = errors;
            IsSuccess = isSuccess;
        }

        public static OperationResult<T> Success(T? data, string? message = null) =>
            new OperationResult<T>(data ?? default, message, true, null);

        public static OperationResult<T> Failure(string message, List<string>? errors, T? data) =>
            new OperationResult<T>(data ?? default, message, false, errors ?? new List<string>());
    }
}