
public class BookService : IBookService
{
    private readonly IBaseService _baseService;

    public BookService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto?> CreateBookAsync(BookDto bookDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDto?> GetAllBooksAsync()
    {
        var requestDto = new RequestDto
        {
            RequestType = Utilities.RequestType.GET,
            Url = Utilities.BooksAPIBaseAddress + ("/books")
        };

        return await _baseService.SendAsync(requestDto);

    }

    public async Task<ResponseDto?> GetBookByIdAsync(int bookId)
    {
        throw new NotImplementedException();
    }
}