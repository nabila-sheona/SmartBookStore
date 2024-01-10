
using smartbookstore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace smartbookstore
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            ILibraryService libraryService = new Library();
            IUserService userService = new UserService(libraryService);
            IAdminService adminService = new AdminService(libraryService);
            Icheckuserid checkuseridservice = new CheckuserID();
            IIAddtoCart addtoCart = new Addtocart(libraryService);
            IGenerateNewUser generateNewUser=new GenerateNewUser();
           IBorrowReturnService borrowReturnService=new BorrowReturnService(userService, libraryService, checkuseridservice);


            SmartBookstore smartBookstore = new SmartBookstore(userService, libraryService, adminService, checkuseridservice, borrowReturnService, addtoCart, generateNewUser);
            smartBookstore.Run();

        }
    }
}




