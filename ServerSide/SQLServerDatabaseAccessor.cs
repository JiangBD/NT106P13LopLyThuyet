using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace TCPServer;

using System.Collections;
using System.Security.Cryptography;
public class SQLServerDatabaseAccessor
{
    private static readonly object lockObject = new();
    private SQLServerDatabaseAccessor() { }
    private static SQLServerDatabaseAccessor instance;
    private SqlConnection connection;
    public static void CreateAccessor()
    {   if ( instance == null )
        {
            
            instance = new SQLServerDatabaseAccessor();
            string workingDirectory = Directory.GetCurrentDirectory();            
            
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;           
            
            string databaseFilePath = Path.Combine(projectDirectory, @"MyApp.mdf");
            
            string connectionString = 
            $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Integrated Security=True;";
            instance.connection = new SqlConnection(connectionString);
            instance.connection.Open();
        }
    }
    public static Dictionary<string,User> GetAllUsers()
    {
        Dictionary<string, User> dict = new Dictionary<string, User>(); 
            try
            {
                string query = "SELECT Username, PasswordHash, FullName, Email, DateOfBirth  FROM Users";
                
                using (SqlCommand command = new SqlCommand(query, instance.connection))
                {
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Read the data from each row
                                string username = reader.GetString(0);
                                string passwordHash = reader.GetString(1);
                                string fullname = reader.GetString(2);
                                string email = reader.GetString(3);           
                                DateTime dt = new DateTime(int.Parse(reader.GetString(4).Split(" ")[2])  /// year, month, day
                                    , int.Parse(reader.GetString(4).Split(" ")[1]), int.Parse(reader.GetString(4).Split(" ")[0]));
                                User u = new User(username, passwordHash, fullname,email, dt);
                                if (!dict.ContainsKey(u.UserName)) dict.Add(username, u);
                            }
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                Console.WriteLine("Error: " + ex.Message);
            }
        return dict;
        
    }
    public static void InsertNewUser(User newUser)
    {
        lock (lockObject)
        {
            string insertDataQuery =
            "INSERT INTO Users (Username, PasswordHash,FullName, Email, DateOfBirth)" +
            " VALUES (@Username, @Password,@FullName, @Email, @DateOfBirth)";

            using (SqlCommand command = new SqlCommand(insertDataQuery, instance.connection))
            {
                // Add parameters to prevent SQL injection and safely insert data
                // command.Parameters.AddWithValue("@UserID", CountUsers() + 1);
                command.Parameters.AddWithValue("@Username", newUser.UserName);
                command.Parameters.AddWithValue("@Password", (newUser.PasswordHash));
                command.Parameters.AddWithValue("@FullName", newUser.FullName);
                command.Parameters.AddWithValue("@Email", newUser.Email);
                string dobstr = newUser.DoB.Day.ToString() + " " + newUser.DoB.Month.ToString()
                    + " " + newUser.DoB.Year.ToString();
                command.Parameters.AddWithValue("@DateOfBirth",dobstr);
                // Execute the insert command
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) inserted.");
            }
        }
    }
    private static int CountUsers()
    {   lock (lockObject)
        {
            string query = "SELECT COUNT(*) FROM Users";
            int userCount = 0;
            SqlCommand command = new SqlCommand(query, instance.connection);
            userCount = (int)command.ExecuteScalar();
            return userCount;
        }
    }
    public static bool ExistsUserName(string userName)
    {
        lock (lockObject)
        {
            string query = "SELECT 1 FROM Users WHERE UserName = @userName";

            SqlCommand command = new SqlCommand(query, instance.connection);
            command.Parameters.AddWithValue("@userName", userName);
            var result = command.ExecuteScalar();

            if (result != null)
                return true;
            else return false;
        }
    }
    public static void UpdatePassword(string targetUserName, string newPasswordHash)
    {
        //string connectionString = "YourConnectionStringHere"; // Replace with your connection string
        //string newPassword = "thisisnewpassword";
        //string targetUserName = "nhockute24";

        string query = "UPDATE Users SET PasswordHash = @PasswordHash WHERE UserName = @UserName";

        
            SqlCommand command = new SqlCommand(query, instance.connection);
            command.Parameters.AddWithValue("@PasswordHash", newPasswordHash);
            command.Parameters.AddWithValue("@UserName", targetUserName);

            try
            {                
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0
                    ? "Password updated successfully."
                    : "No user found with the specified UserName.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        
    }

    public static void Close()
    {
        instance.connection.Close();
    }
    public static string ComputeSha256Hash(string rawData)
    {        
        using (SHA256 sha256Hash = SHA256.Create())
        {            
            byte[] bytes = sha256Hash.ComputeHash(Encoding.ASCII.GetBytes(rawData));
            
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }            
            return builder.ToString(); 
        }
    }
   
}
