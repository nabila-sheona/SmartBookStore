using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartbookstore
{
    public interface ILibraryService
    {
  
        void DisplayAvailableBooks();
        void DisplayAllBooks();
        void DisplayBooksbyGenre();
        void AddBookToLibrary(string title, string author, Genre genre, double price, int quantity);
        void AddinCart(string title, int quantity);
        //void AddToCart();
       // void ReturnBookToLibrary(Book book, string userid);
        void IncreaseBookQuantity(string bookTitle);
        void DecreaseBookQuantity(string bookTitle);
        void Checkout();
        void RemoveBookFromLibrary(string titleToRemove);
        List<Book> GetAvailableBooks();

      
    }
}
