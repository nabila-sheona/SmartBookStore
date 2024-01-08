using smartbookstore;
using System;

public class BorrowReturnService : IBorrowReturnService
{
    private readonly IUserService userService;
    private readonly ILibraryService libraryService;
    private readonly Icheckuserid checkUserService;

    public BorrowReturnService(IUserService userService, ILibraryService libraryService, Icheckuserid checkUserService)
    {
        this.userService = userService;
        this.libraryService = libraryService;
        this.checkUserService = checkUserService;
    }

    public void BorrowBook()
    {
        Console.WriteLine("Are you a library member? (yes/no)");
        string response = Console.ReadLine();

        if (response.Equals("yes", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Enter your user ID:");
            string userId = Console.ReadLine();

            bool isValidUserId = checkUserService.CheckUserId(userId);

            if (isValidUserId)
            {
                Console.WriteLine("\nEnter the title of the book you want to borrow:");
                string title = Console.ReadLine();
                Book selectedBook = libraryService.GetAvailableBooks().Find(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

                if (selectedBook != null && selectedBook.Quantity > 0)
                {
                    userService.BorrowBook(selectedBook, userId);
                    Console.WriteLine($"You have successfully borrowed '{selectedBook.Title}'.");
                }
                else
                {
                    Console.WriteLine("Sorry, the selected book is not available for borrowing.");
                }
            }
            else
            {
                Console.WriteLine("Invalid user ID. Please enter a valid user ID.");
            }
        }
        // ... (code for 'no' and other cases)
    }

    public void ReturnBook()
    {
        Console.WriteLine("\nUserID: ");
        string userId = Console.ReadLine();
        bool isValidUserId = checkUserService.CheckUserId(userId);

        if (isValidUserId && userService.BorrowedBooks.Count > 0)
        {
            Console.WriteLine("\nSelect a book to return:");
            int index = 1;
            foreach (var book in userService.BorrowedBooks)
            {
                Console.WriteLine($"{index}. {book.Title}");
                index++;
            }

            if (int.TryParse(Console.ReadLine(), out int returnChoice) && returnChoice >= 1 && returnChoice <= userService.BorrowedBooks.Count)
            {
                Book returningBook = userService.BorrowedBooks[returnChoice - 1];
                userService.ReturnBook(returningBook, userId);
                Console.WriteLine($"You have successfully returned '{returningBook.Title}'.");
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a valid number.");
            }
        }
        else
        {
            Console.WriteLine("You haven't borrowed any books yet.");
        }
    }
}
