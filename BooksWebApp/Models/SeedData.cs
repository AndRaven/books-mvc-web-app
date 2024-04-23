using BooksWebApp.Data;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcBookContext(
            serviceProvider.GetRequiredService<DbContextOptions<MvcBookContext>>()))
        {
            // Look for existing books
            if (context.Book.Any())
            {
                return;   // DB has been seeded
            }

            context.Book.AddRange(
                new Book()
                {
                    Title = "A Single Thread",
                    Author = "Tracy Chevalier",
                    Description = "A Single Thread is a timeless story of friendship, love, and a woman crafting her own life.",
                    Url = "https://www.goodreads.com/book/show/43943459-a-single-thread",
                    CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1554183509i/43943459.jpg",
                    Pages = 336,
                    Genre = "Fiction",
                    Language = "English",
                    Year = 2019

                },
                new Book
                {
                    Title = "Lady Tan's circle of women",
                    Author = "Lisa See",
                    Description = "A captivating story of women helping other women.",
                    Url = "https://www.goodreads.com/book/show/62919732-lady-tan-s-circle-of-women",
                    CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1683735261i/62919732.jpg",
                    Pages = 368,
                    Genre = "Fiction",
                    Language = "English",
                    Year = 2023
                },
                new Book
                {
                    Title = "Cutting for stone",
                    Author = "Abraham Verghese",
                    Description = "A sweeping, emotionally riveting first novel - an enthralling family saga of Africa and America, doctors and patients, exile and home.",
                    Url = "https://www.goodreads.com/book/show/3591262-cutting-for-stone",
                    CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1553715166i/3591262.jpg",
                    Pages = 560,
                    Genre = "Fiction",
                    Language = "English",
                    Year = 2008
                },
                new Book
                {
                    Title = "Year of Wonders",
                    Author = "Geraldine Brooks",
                    Description = "Inspired by the true story of Eyam, a village in the rugged hill country of England, Year of Wonders is a richly detailed evocation of a singular moment in history.",
                    Url = "https://www.goodreads.com/book/show/4965.Year_of_Wonders",
                    CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1678314640i/4965.jpg",
                    Pages = 304,
                    Genre = "Fiction",
                    Language = "English",
                    Year = 2001
                }
            );
            context.SaveChanges();
        }
    }
}
