
public interface IBookService
{
    Task<ResponseDto?> GetAllBooksAsync();

    Task<ResponseDto?> GetBookByIdAsync(int bookId);

    Task<ResponseDto?> CreateBookAsync(BookDto bookDto);

}