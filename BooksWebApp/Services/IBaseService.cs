public interface IBaseService
{
    Task<ResponseDto?> SendAsync(RequestDto requestDto);
}