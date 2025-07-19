using Newtonsoft.Json;

namespace StansAssociates_Backend.Models
{
    public class APIResponse
    {
        public APIResponse(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public APIResponse()
        {

        }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
    }



    public class ServiceResponse<T> : APIResponse
    {
        public ServiceResponse(string message, int statusCode, T data) : base(message, statusCode)
        {
            Data = data;
        }

        public ServiceResponse(string message, int statusCode) : base(message, statusCode)
        {

        }

        public ServiceResponse()
        {

        }

        [JsonProperty("data")]
        public T Data { get; set; }
    }



    public class PagedResponseInput
    {
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("searchString")]
        public string? SearchString { get; set; }

        public string FormattedSearchString()
        {
            return SearchString?.ToLower()?.Replace(" ", string.Empty);
        }
    }


    public class PagedResponseWithQuery<T>
    {
        [JsonProperty("totalRecords")]
        public int TotalRecords { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }



    public class PagedResponse<T> : ServiceResponse<T>
    {
        public PagedResponse(string message, int statusCode) : base(message, statusCode)
        {

        }

        public PagedResponse(string message, int statusCode, T data, int pageIndex, int pageSize, int totalRecords) : base(message, statusCode, data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("totalRecords")]
        public int TotalRecords { get; set; }

    }
}
