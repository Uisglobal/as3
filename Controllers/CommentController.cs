using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Assignment_3_APIs.Models
{
    public class CommentData
    {
        private readonly string _connectionString;

        public CommentData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conn");
        }

        // Get all comments
        public List<Comment> GetComments()
        {
            List<Comment> comments = new List<Comment>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM comments";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Comment comment = new Comment
                        {
                            Id = Convert.ToInt32(reader["comment_id"]),
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            UserId = Convert.ToInt32(reader["user_id"]),
                            Rating = Convert.ToInt32(reader["rating"]),
                            Text = reader["text"].ToString()
                        };
                        comments.Add(comment);
                    }
                }
            }

            return comments;
        }

        // Add comment
        public int AddComment(Comment comment)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO comments (product_id, user_id, rating, text) VALUES (@ProductId, @UserId, @Rating, @Text)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", comment.ProductId);
                command.Parameters.AddWithValue("@UserId", comment.UserId);
                command.Parameters.AddWithValue("@Rating", comment.Rating);
                command.Parameters.AddWithValue("@Text", comment.Text);
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.LastInsertedId);
            }
        }

        // Update comment
        public void UpdateComment(int id, Comment comment)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE comments SET product_id = @ProductId, user_id = @UserId, rating = @Rating, text = @Text WHERE comment_id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", comment.ProductId);
                command.Parameters.AddWithValue("@UserId", comment.UserId);
                command.Parameters.AddWithValue("@Rating", comment.Rating);
                command.Parameters.AddWithValue("@Text", comment.Text);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        // Delete comment
        public void DeleteComment(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM comments WHERE comment_id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
