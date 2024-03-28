using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Assignment_3_APIs.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace Assignment_3_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CartController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("SQLiteConnection")))
            {
                var query = "SELECT * FROM cart";
                var adapter = new MySqlDataAdapter(query, connection);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return new JsonResult(dataTable);
            }
        }

        [HttpPost]
        public JsonResult Post(Cart cart)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("SQLiteConnection")))
            {
                var query = "INSERT INTO cart (product_id, quantity, user_id) VALUES (@ProductId, @Quantity, @UserId)";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", cart.ProductId);
                command.Parameters.AddWithValue("@Quantity", cart.Quantity);
                command.Parameters.AddWithValue("@UserId", cart.UserId);
                connection.Open();
                command.ExecuteNonQuery();
                return new JsonResult("Added Successfully");
            }
        }

        [HttpPut("{id}")]
        public JsonResult Put(int id, Cart cart)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("SQLiteConnection")))
            {
                var query = "UPDATE cart SET product_id = @ProductId, quantity = @Quantity, user_id = @UserId WHERE id = @Id";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", cart.ProductId);
                command.Parameters.AddWithValue("@Quantity", cart.Quantity);
                command.Parameters.AddWithValue("@UserId", cart.UserId);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
                return new JsonResult("Updated Successfully");
            }
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("SQLiteConnection")))
            {
                var query = "DELETE FROM cart WHERE id = @Id";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
                return new JsonResult("Deleted Successfully");
            }
        }
    }
}
