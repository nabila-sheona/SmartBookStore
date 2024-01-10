
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartbookstore
{
    public class UserModule
    {
        private readonly IUserService userService;
        private readonly ILibraryService libraryService;
        private readonly Icheckuserid checkuseridservice;
        private readonly IBorrowReturnService borrowReturnService;
        private readonly IIAddtoCart addtoCartService;
        private readonly IGenerateNewUser generateNewUserService;

        public UserModule(IUserService userService, ILibraryService libraryService, Icheckuserid checkuseridservice, IBorrowReturnService borrowReturnService, IIAddtoCart addtoCartService, IGenerateNewUser generateNewUserService)
        {
            this.userService = userService;
            this.libraryService = libraryService;
            this.checkuseridservice = checkuseridservice;
            this.borrowReturnService = borrowReturnService;
            this.addtoCartService = addtoCartService;
            this.generateNewUserService = generateNewUserService;
        }

        public void UserRun()
        {
           
                while (true)
                {

                    Console.WriteLine("\nSelect an option:");
                    Console.WriteLine("1. Browse available books");
                    Console.WriteLine("2. Browse available books by genre");
                    Console.WriteLine("3. Become a library member");
                    Console.WriteLine("4. Borrow a book");
                    Console.WriteLine("5. Return a book");
                    Console.WriteLine("6. Add to cart");
                    Console.WriteLine("7. Checkout");
                    Console.WriteLine("8. Exit");

                  
                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                       switch (choice)
                       { 
     
                        case 1:
                             libraryService.DisplayAvailableBooks();
                             break;

                        case 2:
                            libraryService.DisplayBooksbyGenre();
                            break;
                        case 3:

                                generateNewUserService.BecomeMember();
                                Console.WriteLine("You are now a library member!");
                                break;
                        case 4:
                                borrowReturnService.BorrowBook();
                                break;
                        case 5:
                                borrowReturnService.ReturnBook();
                                break;
                      
                        case 6:

                               addtoCartService.AddBookToCart(libraryService);
                                break;
                        case 7:
                                libraryService.Checkout();
                                break;

                        case 8:
                                Console.WriteLine("Thank you for using the Online Bookstore. Goodbye!");
                                Environment.Exit(0);
                                break;


                        default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }


        }
     

    }
}
