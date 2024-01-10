using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartbookstore
{
    public enum Genre
    {
        Fiction,
        Mystery,
        Scifi,
        Romance,
        Classic,
        Fantasy,
        NonFiction,
        Youngadult
        // Add more genres as needed
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool IsBorrowed { get; set; }

        public Genre BookGenre { get; set; }

        public bool IsAvailable()
        {
            return Quantity >0;
        }

        public void BorrowBook()
        {
            if (IsAvailable())
            {
                IsBorrowed = true;
                
            }
        }

        public void ReturnBook()
        {
            IsBorrowed = false;
        }

        public void SellBook()
        {
            Quantity--;
        }
    }

}
