using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace smartbookstore
{
    public class SmartBookstore
    {
        

        private readonly AdminModule adminModule;
        private readonly UserModule userModule;
      

        public SmartBookstore(IUserService userService, ILibraryService libraryService, IAdminService adminService, Icheckuserid checkuseridservice, IBorrowReturnService borrowReturnService, IIAddtoCart addtoCart, IGenerateNewUser generateNewUser)
        {
            adminModule = new AdminModule(libraryService, adminService);
            userModule = new UserModule(userService, libraryService, checkuseridservice, borrowReturnService, addtoCart, generateNewUser);
        
            
            
        }
        public void Run()
        {
            Console.WriteLine("Welcome to the Smart Bookstore!");

            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Admin Module");
                Console.WriteLine("2. User Module");
                Console.WriteLine("3. Exit");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            adminModule.RunAdminModule();
                            break;
                        case 2:
                            userModule.UserRun();
                            break;
                        case 3:
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
