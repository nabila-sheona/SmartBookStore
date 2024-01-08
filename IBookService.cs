using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartbookstore
{

    public interface IBookService
    {
        void BorrowBook(UserService user, Book book);
        void ReturnBook(UserService user, Book book);
        double CalculateBorrowingFee(Book book);
        void SellBook(Book book);
    }


}
