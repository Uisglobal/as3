using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Assignment_3_APIs.Models
{
    public class Orders
    {
        private readonly string _connectionString;

        public Orders(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLiteConnection");
        }

        // Get all orders
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM orders";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            OrderId = Convert.ToInt32(reader["order_id"]),
                            Id = Convert.ToInt32(reader["user_id"]),
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            Quantity = Convert.ToInt32(reader["quantity"]),
                            Total = Convert.ToDecimal(reader["total_price"]),
                            CreatedDate = Convert.ToDateTime(reader["order_date"])
                        };
                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        // Add order
        public int AddOrder(Order order)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO orders (user_id, product_id, quantity, total_price, order_date) VALUES (@UserId, @ProductId, @Quantity, @TotalPrice, @OrderDate)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", order.UserId);
                command.Parameters.AddWithValue("@ProductId", order.ProductId);
                command.Parameters.AddWithValue("@Quantity", order.Quantity);
                command.Parameters.AddWithValue("@TotalPrice", order.Total);
                command.Parameters.AddWithValue("@OrderDate", order.CreatedDate);
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.LastInsertedId);
            }
        }

        // Update order
        public void UpdateOrder(int id, Order order)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE orders SET user_id = @UserId, product_id = @ProductId, quantity = @Quantity, total_price = @TotalPrice, order_date = @OrderDate WHERE order_id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", order.UserId);
                command.Parameters.AddWithValue("@ProductId", order.ProductId);
                command.Parameters.AddWithValue("@Quantity", order.Quantity);
                command.Parameters.AddWithValue("@TotalPrice", order.Total);
                command.Parameters.AddWithValue("@OrderDate", order.CreatedDate);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        // Delete order
        public void DeleteOrder(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM orders WHERE order_id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
