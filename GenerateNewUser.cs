using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlinebookstore
{
    public class GenerateNewUser: IGenerateNewUser
    {
        public bool isLibraryMember;
        public string username;
        public int numberofuser;
        private string userId;
       public GenerateNewUser() {

            LoadUserInformationFromFile();
        
        }

        public void BecomeMember()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            username = name;
            isLibraryMember = true;
            GenerateUserId(name);
            SaveUserInformationToFile(userId, username);
            Console.WriteLine("You are now a library member!");
        }


        private void GenerateUserId(string name)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            int usersIn = GetUserNumber();

            userId = $"{year}_{month:D2}_{usersIn:D4}_{name.Substring(0, 2)}";
            Console.WriteLine($"Your library member ID is: {userId}");
        }

        private int GetUserNumber()
        {
            string filePath = "UserData.txt";

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                numberofuser = lines.Length;
            }

            return numberofuser;
        }

        private void SaveUserInformationToFile(string userId, string username)
        {
            try
            {
                string filePath = "UserData.txt";

                File.AppendAllLines(filePath, new[] { $"{userId},{username}" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user information to text file: {ex.Message}");
            }
        }

        // New method to load user information from a text file
        private void LoadUserInformationFromFile()
        {
            try
            {
                // Specify the path to your text file
                string filePath = "UserData.txt";

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Read all lines from the text file and do any necessary processing
                    string[] lines = File.ReadAllLines(filePath);

                    // You can process the lines if needed
                }
                else
                {
                    Console.WriteLine("User information file does not exist. No users have been added yet.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., file not found, permission issues)
                Console.WriteLine($"Error loading user information from text file: {ex.Message}");
            }
        }
    }
}
