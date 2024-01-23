using H_Sports.Interfaces;
using H_Sports.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient; 

namespace H_Sports.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public List<User> GetUsers()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())

                {
                    cmd.CommandText = "SELECT Id, UserName, Email, FirstName, LastName FROM [User] ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var users = new List<User>();
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName"))
                            };
                            users.Add(user);
                        }
                        return users;
                    }
                }
            }
        }
        public User GetUserByUserName (string userName) {


            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())

                {
                    cmd.CommandText = @"Select Id, UserName, Email, FirstName, LastName FROM [User]
                                        where UserName= @UserName ";
                    cmd.Parameters.AddWithValue ("username", userName);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName"))
                            };
                            return user;
                        }
                        
                    }
                }
            }
            return null;
        }
        public void CreateUser(User newUser)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [User] (UserName, Email, FirstName, LastName)
                                        VALUES (@UserName, @Email, @FirstName, @LastName)";

                    cmd.Parameters.AddWithValue("@UserName", newUser.UserName);
                    cmd.Parameters.AddWithValue("@Email", newUser.Email);
                    cmd.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", newUser.LastName);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        User IUserRepository.CreateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}