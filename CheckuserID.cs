using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace smartbookstore
{
    public class CheckuserID: Icheckuserid
    {

        public bool CheckUserId(string userId)
        {
            try
            {
                // Specify the path to your user information file
                string filePath = "UserData.txt";

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Read all lines from the text file
                    string[] lines = File.ReadAllLines(filePath);

                    // Check if the provided userId is in the list of user IDs
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        string loadedUserId = parts[0].Trim(); // Trim to remove extra spaces

                        // Make the comparison case-insensitive
                        if (string.Equals(loadedUserId, userId, StringComparison.OrdinalIgnoreCase))
                        {
                            // UserId found in the file
                            return true;
                        }
                    }

                    // UserId not found in the file
                    return false;
                }
                else
                {
                    Console.WriteLine("User information file does not exist. No users have been added yet.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., file not found, permission issues)
                Console.WriteLine($"Error checking user ID in text file: {ex.Message}");
                return false;
            }
        }
    }
}
