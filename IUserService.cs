using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartbookstore
{
    public interface IUserService
    {
       // void BecomeMember();
        List<Book> BorrowedBooks { get; }

        void ReturnBook(Book book, string userid);
        void BorrowBook(Book book, string userid);
        List<Book> GetBorrowedBooks();
        //bool IsLibraryMember();

    }
}
