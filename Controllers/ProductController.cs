using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Assignment_3_APIs.Models
{
    public class ProductData
    {
        private readonly string _connectionString;

        public ProductData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conn");
        }

        // Get products list
        public List<Product> GetProductList()
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM products";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = Convert.ToInt32(reader["product_id"]),
                            Price = Convert.ToDecimal(reader["pricing"]),
                            Description = reader["description"].ToString(),
                            Image = reader["image"].ToString(),
                            ShippingCost = Convert.ToDecimal(reader["shipping_cost"])
                        };
                        products.Add(product);
                    }
                }
            }

            return products;
        }

        // Add product
        public int AddProduct(Product product)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO products (pricing, description, image, shipping_cost) VALUES (@Pricing, @Description, @Image, @ShippingCost)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Pricing", product.Price);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Image", product.Image);
                command.Parameters.AddWithValue("@ShippingCost", product.ShippingCost);
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.LastInsertedId);
            }
        }

        // Update product
        public void UpdateProduct(int id, Product product)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE products SET pricing = @Pricing, description = @Description, image = @Image, shipping_cost = @ShippingCost WHERE product_id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Pricing", product.Price);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Image", product.Image);
                command.Parameters.AddWithValue("@ShippingCost", product.ShippingCost);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        // Delete product
        public void DeleteProduct(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM products WHERE product_id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
