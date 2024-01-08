using smartbookstore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartbookstore
{
    public class Addtocart: IIAddtoCart
    {
        private readonly ILibraryService libraryService;
        public Addtocart(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }
        public void AddBookToCart(ILibraryService libraryService)
        {
            Console.WriteLine("\nEnter the title of the book you want to add to the cart:");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Invalid title. Please enter a valid title.");
                return;
            }

            Console.WriteLine("Enter the quantity:");

            //if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
            //{
            
              int quantity= Convert.ToInt16( Console.ReadLine());
            if(quantity> 0) { 
                libraryService.AddinCart(title, quantity);
                Console.WriteLine($"{quantity} copies of '{title}' added to the cart.");
            }
            else
            {
                Console.WriteLine("Invalid quantity. Please enter a valid number greater than 0.");
            }
        }
    }
}
