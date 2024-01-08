using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartbookstore
{
    public class AdminModule
    {
        private readonly ILibraryService libraryService;
        private readonly IAdminService adminService;

        public AdminModule(ILibraryService libraryService, IAdminService adminService)
        {
            this.libraryService = libraryService;
            this.adminService = adminService;
        }
        public void RunAdminModule()
        {
            while (true)
            {
                Console.WriteLine("1. Add a new book to the library");
                Console.WriteLine("2. Exit");
                Console.WriteLine("3. Browse books");
                //Console.WriteLine("4. Remove book");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:

                           adminService.AddBookToLibrary();
                            break;
                        case 2:
                            Console.WriteLine("Thank you for using the Online Bookstore. Goodbye!");
                            Environment.Exit(0);
                            break;
                        case 3:

                            libraryService.DisplayAllBooks();
                            break;
                        case 4:
                            adminService.RemoveBookFromLibrary();
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
