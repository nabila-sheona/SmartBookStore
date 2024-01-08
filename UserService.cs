using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartbookstore
{
    public class UserService : IUserService
    {
        private bool isLibraryMember;
        public List<Book> borrowedBooks = new List<Book>();
        public ILibraryService libraryService;
        public List<Book> BorrowedBooks => borrowedBooks;
        public string username;
        public int numberofuser;
        private string userId;

        public UserService(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
            //LoadUserInformationFromFile();
        }

        private void SaveBorrowedBooksData(Book book, string userid, bool isreturned)
        {
            // Specify the path to your borrowed books data file
            string borrowedBooksFilePath = "BorrowedBooksData.txt";

            try
            {
                // Create or append to the borrowed books data file
                using (StreamWriter sw = File.AppendText(borrowedBooksFilePath))
                {

                    // Save book information along with borrowing date, user ID, and is returned status
                    sw.WriteLine($"{book.Title},{DateTime.Now},{userId},{isreturned}");

                }

                Console.WriteLine("Book data saved.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., file not found, permission issues)
                Console.WriteLine($"Error saving borrowed books data: {ex.Message}");
            }
        }

        public void BorrowBook(Book book, string userid)
        {
            if (book.IsAvailable())
            {
                book.BorrowBook(); // This method should set IsBorrowed to true
                SaveBorrowedBooksData(book, userid, false);
                string borrowed = book.Title.ToString();
                libraryService.DecreaseBookQuantity(borrowed);
                borrowedBooks.Add(book);
                Console.WriteLine($"You have successfully borrowed '{book.Title}'.");
            }
            else
            {
                Console.WriteLine($"Unable to borrow '{book.Title}'.");
            }
           
        }
       
        public void ReturnBook(Book book, string userid)  
        {
            if (book.IsBorrowed)
            {
                string borrowed = book.Title.ToString();
                libraryService.IncreaseBookQuantity(borrowed);


                // Calculate the number of days the book has been borrowed
                int daysBorrowed = CalculateDaysBorrowed(book);

                // Calculate the amount to be paid
                double amountToPay = CalculateAmountToPay(daysBorrowed);
                book.IsBorrowed = false;

                // Add the book back to the library

                // Update the list of borrowed books
                

                Console.WriteLine($"You have successfully returned '{book.Title}'.");
                Console.WriteLine($"Amount to pay: ${amountToPay}");
                
                // Save the updated list of borrowed books to the user's file
                SaveBorrowedBooksData(book, userid, true);
                

                // Update the book's availability

            }
            else
            {
                Console.WriteLine($"You haven't borrowed '{book.Title}'.");
            }
        }
        private int CalculateDaysBorrowed(Book book)
        {
            // Specify the path to your borrowed books data file
            string borrowedBooksFilePath = "BorrowedBooksData.txt";

            try
            {
                // Read all lines from the borrowed books data file
                string[] lines = File.ReadAllLines(borrowedBooksFilePath);

                foreach (string line in lines)
                {
                    // Split the line into parts
                    string[] parts = line.Split(',');

                    // Extract the book title, borrowing date, and user ID
                    string bookTitle = parts[0].Trim();
                    DateTime borrowingDate = DateTime.Parse(parts[1].Trim());
                    string userId = parts[2].Trim();

                    // Check if the current line corresponds to the given book
                    if (bookTitle.Equals(book.Title, StringComparison.OrdinalIgnoreCase))
                    {
                        // Calculate the number of days between the borrowing date and today
                        int daysBorrowed = (DateTime.Now - borrowingDate).Days;

                        // Return the calculated number of days
                        return daysBorrowed;
                    }
                }

                // If no match is found, return a default value (0)
                return 0;
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., file not found, permission issues)
                Console.WriteLine($"Error calculating days borrowed: {ex.Message}");

                // Return a default value (0)
                return 0;
            }
        }


        private double CalculateAmountToPay(int daysBorrowed)
        {
            double dailyRate = 0.5;
            double additionalRate = 1.0;

            if (daysBorrowed <= 15)
            {
                return dailyRate * daysBorrowed;
            }
            else
            {
                return (dailyRate * 15) + (additionalRate * (daysBorrowed - 15));
            }
        }

        
        public bool IsLibraryMember()
        {
            return isLibraryMember;
        }

  
        
        public List<Book> GetBorrowedBooks()
        {
            return borrowedBooks;
        }

    }

}
