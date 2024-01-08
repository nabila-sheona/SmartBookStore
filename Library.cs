using smartbookstore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartbookstore
{
    public class Library : ILibraryService
    {
        private readonly List<Tuple<string, string>> borrowedBooks = new List<Tuple<string, string>>();
        private List<Book> Books = new List<Book>();
        private List<Book> ShoppingCart = new List<Book>();
        private List<Book> SoldBooks = new List<Book>();
        private string filePath = "BookData.txt";
        private string soldBooksFilePath = "SoldBooksData.txt";

        // Constructor: Load existing book data when initializing the Library
        public Library()
        {
            LoadBookData();
            LoadSoldBooksData();
        }

        public List<Book> GetAvailableBooks()
        {
            return Books.FindAll(book => book.Quantity > 0);
        }
      

        public void DisplayAllBooks()
        {

            Console.WriteLine("All Books:");
            foreach (Book book in GetAvailableBooks())
            {
                Console.WriteLine($"{book.Title} by {book.Author} - ${book.Price} (Available: {book.Quantity})");
            }
        }
        public void DisplayAvailableBooks()
        {

            Console.WriteLine("\nAvailable Books:");
            foreach (Book book in GetAvailableBooks())
            {
                Console.WriteLine($"{book.Title} by {book.Author} - ${book.Price} (Available: {book.Quantity})");
            }
        }


        public void AddBookToLibrary(string title, string author, double price, int quantity)
        {
            string pprice = price.ToString();
            // Check if the book already exists in the library
            Book existingBook = Books.FirstOrDefault(book =>
                book.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
                book.Author.Equals(author, StringComparison.OrdinalIgnoreCase) &&
                book.Price.Equals(price));

            if (existingBook != null)
            {
                // If the book exists, increase the quantity
                existingBook.Quantity += quantity;
                Console.WriteLine($"{quantity} more copies of '{title}' by {author} added to the library. Total quantity: {existingBook.Quantity}");
            }
            else
            {
                // If the book doesn't exist, add a new entry
                Book newBook = new Book
                {
                    Title = title,
                    Author = author,
                    Price = price,
                    Quantity = quantity,
                    IsBorrowed = false
                };

                Books.Add(newBook);
                Console.WriteLine($"{quantity} copies of '{title}' by {author} added to the library.");
            }

            // Save the updated book data to the text file after adding or updating books
            SaveBookData();
        }



        public void LoadBookData()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    // Read existing book data from the text file
                    string[] lines = File.ReadAllLines(filePath);

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 5)
                        {
                            Book book = new Book
                            {
                                Title = parts[0],
                                Author = parts[1],
                                Price = double.Parse(parts[2]),
                                Quantity = int.Parse(parts[3]),
                                IsBorrowed = bool.Parse(parts[4])
                            };

                            Books.Add(book);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading book data: {ex.Message}");
            }
        }

        public void SaveBookData()
        {
            try
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    foreach (Book book in Books)
                    {
                        sw.WriteLine($"{book.Title},{book.Author},{book.Price},{book.Quantity},{book.IsBorrowed}");
                        //sw.Flush();
                    }
                }

                Console.WriteLine("Book data saved successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving book data: {ex.Message}");
            }
        }


        private void LoadSoldBooksData()
        {
            if (File.Exists(soldBooksFilePath))
            {
                string[] lines = File.ReadAllLines(soldBooksFilePath);
                foreach (string line in lines)
                {
                    string[] values = line.Split(',');
                    if (values.Length == 5)
                    {
                        SoldBooks.Add(new Book
                        {
                            Title = values[0],
                            Author = values[1],
                            Price = Convert.ToDouble(values[2]),
                            Quantity = Convert.ToInt32(values[3]),
                            IsBorrowed = Convert.ToBoolean(values[4])
                        });
                    }
                }
            }
        }

        // New method to save sold books data to the text file
        private void SaveSoldBooksData(double totalBill)
        {
            List<string> lines = SoldBooks.Select(book =>
                $"{book.Title},{book.Author},{book.Price},{book.Quantity},{book.IsBorrowed}").ToList();

            // Append total bill to the file
            lines.Add($"TotalBill,{totalBill}");

            File.WriteAllLines(soldBooksFilePath, lines);
        }


        public void AddinCart(string title, int quantity)
        {
            // Check if the book exists in the library
            Book existingBook = Books.FirstOrDefault(book =>
                book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (existingBook != null && existingBook.Quantity >= quantity)
            {
                // If the book exists and there are enough copies, add to the cart
                Book cartItem = new Book
                {
                    Title = existingBook.Title,
                    Author = existingBook.Author,
                    Price = existingBook.Price,
                    Quantity = quantity,
                    IsBorrowed = false
                };

                ShoppingCart.Add(cartItem);
                Console.WriteLine($"{quantity} copies of '{title}' added to the cart.");
            }
            else
            {
                Console.WriteLine($"Not enough copies of '{title}' available to add to the cart.");
            }
        }



        public void Checkout()
        {
            double totalBill = 0;

            // Check out the items in the cart and update book quantities
            foreach (Book cartItem in ShoppingCart)
            {
                Book existingBook = Books.FirstOrDefault(book =>
                    book.Title.Equals(cartItem.Title, StringComparison.OrdinalIgnoreCase));

                if (existingBook != null && existingBook.Quantity >= cartItem.Quantity)
                {
                    // If the book exists and there are enough copies, update the quantity
                    existingBook.Quantity -= cartItem.Quantity;

                    // Calculate and accumulate the total bill
                    totalBill += cartItem.Price * cartItem.Quantity;

                    // Add the sold book to the SoldBooks list
                    SoldBooks.Add(new Book
                    {
                        Title = existingBook.Title,
                        Author = existingBook.Author,
                        Price = existingBook.Price,
                        Quantity = cartItem.Quantity,
                        IsBorrowed = false
                    });

                    Console.WriteLine($"{cartItem.Quantity} copies of '{cartItem.Title}' checked out successfully. Remaining quantity: {existingBook.Quantity}");
                }
                else
                {
                    Console.WriteLine($"Not enough copies of '{cartItem.Title}' available to check out.");
                }
            }

            // Display and save the total bill
            Console.WriteLine($"Total Bill: ${totalBill}");

            // Clear the cart after checkout
            ShoppingCart.Clear();

            // Save the updated book data, sold books data, and total bill to the text files after checkout
            SaveBookData();
            SaveSoldBooksData(totalBill);
        }


        public void RemoveBookFromLibrary(string title)

        {

            Book bookToRemove = Books.FirstOrDefault(book =>
                book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (bookToRemove != null)
            {
                Books.Remove(bookToRemove);

                // Save the updated book data to the text file after removing
                SaveBookData();
            }
            else
            {
                Console.WriteLine($"Book '{title}' not found in the library.");
            }

        }

       
        public void DecreaseBookQuantity(string bookTitle)
        {
            Book availableBook = GetAvailableBooks().Find(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));

            if (availableBook != null && availableBook.Quantity > 0)
            {
                // Decrease the quantity for the book
                availableBook.Quantity--;

                // Save the updated book data to the text file after borrowing

                SaveBookData(); // Ensure that SaveBookData() is called here

                Console.WriteLine($"Borrowed 1 copy of '{availableBook.Title}'. Remaining quantity: {availableBook.Quantity}");
            }
            else
            {
                Console.WriteLine($"Book '{bookTitle}' not available for borrowing.");
            }
        }

        public void IncreaseBookQuantity(string bookTitle)
        {
            Book availableBook = GetAvailableBooks().Find(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));

            if (availableBook != null && availableBook.Quantity > 0)
            {
                // Decrease the quantity for the book
                availableBook.Quantity++;

                // Save the updated book data to the text file after borrowing
                SaveBookData();

                Console.WriteLine($"Borrowed 1 copy of '{availableBook.Title}'. Remaining quantity: {availableBook.Quantity}");
            }
            else
            {
                Console.WriteLine($"Book '{bookTitle}' not available for borrowing.");
            }
        }

       
    }

}

