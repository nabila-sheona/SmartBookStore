using System;
using System.Collections.Generic;

namespace smartbookstore
{
    public class AdminService : IAdminService
    {
        private List<Book> Books;
        private readonly ILibraryService libraryService;
        public AdminService(ILibraryService libraryService)
        {
            Books = new List<Book>();
            this.libraryService= libraryService;
        }

        public void AddBookToLibrary()
        {
            Console.WriteLine("\nEnter the details of the new book:");
            Console.Write("Enter the title: ");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Invalid title. Please enter a valid title.");
                return;
            }

            Console.Write("Enter the author: ");
            string author = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Invalid author. Please enter a valid author.");
                return;
            }

            Console.WriteLine("Enter the genre (Fiction, Mystery, Scifi, Romance, Classic, Fantasy, NonFiction, Youngadult, etc.):");
            string genreInput = Console.ReadLine();

            if (Enum.TryParse<Genre>(genreInput, out Genre genre))
            {
              //
            }
            else
            {
                Console.WriteLine("Invalid genre. Please enter a valid genre.");
            }

            Console.Write("Enter the price: ");

            if (double.TryParse(Console.ReadLine(), out double price) && price > 0)
            {
                Console.Write("Enter the quantity: ");

                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                {
                    libraryService.AddBookToLibrary(title, author, genre, price, quantity);
                    Console.WriteLine($"{quantity} copies of '{title}' by {author} added to the library.");
                }
                else
                {
                    Console.WriteLine("Invalid quantity. Please enter a valid number greater than 0.");
                }
            }
            else
            {
                Console.WriteLine("Invalid price. Please enter a valid number greater than 0.");
            }


        }

        public void RemoveBookFromLibrary()
        {
            Console.WriteLine("Book to delete: ");
            string title = Console.ReadLine();
             libraryService.RemoveBookFromLibrary(title);
        }
    }
}
