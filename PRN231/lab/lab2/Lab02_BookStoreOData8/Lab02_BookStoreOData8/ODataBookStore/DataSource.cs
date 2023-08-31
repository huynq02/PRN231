using Entity;

namespace ODataBookStore
{
    public static class DataSource
    {
        private static IList<Book> listBooks { get; set; }
        public static IList<Book> GetBooks()
        {
            if(listBooks != null) return listBooks;
            listBooks = new List<Book>();
            Book book = new Book
            {
                Id = "1",
                ISBN = "05-04-0002-12",
                Title = "Learn Java",
                Author = "Jinju",
                Price = 59.99m,
                City = "HCM City",
                Street = "Phu cu, hung yen",
                Press = new Press
                {
                    Id = 1,
                    Name = "Ha noi",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);

            book = new Book
            {
                Id = "2",
                ISBN = "033-0456-00021-122",
                Title = "Learn JC#",
                Author = "Yankee",
                Price = 60.66m,
                City = "Ha noi City",
                Street = "Me tri",
                Press = new Press
                {
                    Id = 2,
                    Name = "Hung Yen",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);
            book = new Book
            {
                Id = "3",
                ISBN = "033-0456-00021-123",
                Title = "Learn Co Chi xinhh gaii",
                Author = "Co Chi",
                Price = 60.99m,
                City = "Long bien City",
                Street = "Gia Lam",
                Press = new Press
                {
                    Id = 3,
                    Name = "Long Bien",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);
            return listBooks;
        }



    }
}
