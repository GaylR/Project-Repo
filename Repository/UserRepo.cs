
using Microsoft.Data.SqlClient;

public class UserRepo
{

    private readonly string _connectionString;

    
    public UserRepo(string connString)
    {
        _connectionString = connString;
    }

      
     public User? GetUser(string Username)
    {
        try
        {
            //Set up DB Connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //Create the SQL String
            string sql = "SELECT * FROM dbo.[USER] WHERE Username = @Username";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@Username", Username);
            

            //Execute the Query
            using var reader = cmd.ExecuteReader();

            //Extract the Results
            if (reader.Read())
            {
               
                User newUser = BuildUser(reader);
                return newUser;
            }

            return null; 

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }

  
    
    //Helper Method
    private static User BuildUser(SqlDataReader reader)
    {
        User newUser = new();
        newUser.Userid = (int)reader["Id"];
        newUser.Username = (string)reader["Username"];
        newUser.Password = (string)reader["Password"];
        

        return newUser;
    }
}



//How to interact with the data (abstracts the data layer) CRUD operations - create, read, update, delet