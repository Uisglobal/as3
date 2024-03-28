using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Assignment_3_APIs.Models
{
    public class UserData
    {
        private readonly string _connectionString;

        public UserData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLiteConnection");

        }

        // Get all user data
        public List<User> GetUserData()
        {
            List<User> users = new List<User>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM users";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            Id = Convert.ToInt32(reader["user_id"]),
                            Email = reader["email"].ToString(),
                            Password = reader["password"].ToString(),
                            Username = reader["username"].ToString(),
                            ShippingAddress = reader["shipping_address"].ToString()
                        };
                        users.Add(user);
                    }
                }
            }

            return users;
        }

        // Add user
        public int AddUser(User user)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                Console.WriteLine("Database connection successful!");

                string query = "INSERT INTO users (email, password, username, purchase_history, shipping_address) VALUES (@Email, @Password, @Username, @PurchaseHistory, @ShippingAddress)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@ShippingAddress", user.ShippingAddress);
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.LastInsertedId);
            }
        }

        // Update user
        public void UpdateUser(int id, User user)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE users SET email = @Email, password = @Password, username = @Username, purchase_history = @PurchaseHistory, shipping_address = @ShippingAddress WHERE user_id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@ShippingAddress", user.ShippingAddress);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        // Delete user
        public void DeleteUser(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM users WHERE user_id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
