using System.Text.Json.Serialization;

namespace University.Domain.Responses
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; } = false;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; } = null;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T? Data { get; set; } = default;

        public BaseResponse(bool success, string? message = null, T? data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public BaseResponse(bool success, T? data = default)
        {
            Success = success;
            Data = data;
        }

        public BaseResponse(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }

        public BaseResponse(bool success)
        {
            Success = success;
        }
    }
}
