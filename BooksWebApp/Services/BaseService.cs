


using System.Net;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

    }
    public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
    {
        try
        {
            //create HTTP client
            var httpClient = _httpClientFactory.CreateClient();


            //create HTTP request
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(requestDto.Url);

            //set HTTP method
            switch (requestDto.RequestType)
            {
                case Utils.RequestType.POST: message.Method = HttpMethod.Post; break;
                case Utils.RequestType.PUT: message.Method = HttpMethod.Put; break;
                case Utils.RequestType.PATCH: message.Method = new HttpMethod("PATCH"); break;
                case Utils.RequestType.DELETE: message.Method = HttpMethod.Delete; break;
                default: message.Method = HttpMethod.Get; break;
            }

            //set HTTP request body if required
            if (requestDto.Data != null)
            {
                string json = JsonConvert.SerializeObject(requestDto.Data);

                //StringContent used to create the content for the HTTP request
                message.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            }

            //send HTTP request and get response
            HttpResponseMessage? apiResponse = await httpClient.SendAsync(message);

            //check HTTP Response
            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new ResponseDto { IsSuccess = false, Message = "Resource not found" };

                case HttpStatusCode.BadRequest:
                    return new ResponseDto { IsSuccess = false, Message = "Bad request" };

                case HttpStatusCode.InternalServerError:
                    return new ResponseDto { IsSuccess = false, Message = "Internal server error" };

                case HttpStatusCode.Forbidden:
                    return new ResponseDto { IsSuccess = false, Message = "Access denied" };

                case HttpStatusCode.Unauthorized:
                    return new ResponseDto { IsSuccess = false, Message = "Unauthorized" };

                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

                    return apiResponseDto;
            }
        }
        catch (Exception ex)
        {
            return new ResponseDto { IsSuccess = false, Message = ex.Message };
        }

    }
}