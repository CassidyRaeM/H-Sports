using H_Sports.Interfaces;
using H_Sports.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Connections;
using static System.Net.Mime.MediaTypeNames;
using System.Linq.Expressions;


namespace H_Sports.Repositories
{
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public ReviewRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public List<Review> GetReviews()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT UserId, ProductId, Text, Id FROM [Review]";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var reviews = new List<Review>();
                        while (reader.Read())
                        {
                            Review review = new Review
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                Text = reader.GetString(reader.GetOrdinal("Text")),
                                Id= reader.GetInt32(reader.GetOrdinal("ID")),   
                            };
                            reviews.Add(review);
                        }
                        return reviews;
                    }
                }
            }
        }
        public int? CreateReview(Review review)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO [Review] (UserId, ProductId, [Text]) OUTPUT INSERTED.ID VALUES (@UserId, @ProductId, @Text)";
                        cmd.Parameters.AddWithValue("@UserId", review.UserId);
                        cmd.Parameters.AddWithValue("@ProductId", review.ProductId);
                        cmd.Parameters.AddWithValue("@Text", review.Text);

                        int Id = (int)cmd.ExecuteScalar();
                        return Id;
                    }
                }

                Console.WriteLine("Review added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public Review GetReviewById(int? Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())

                {
                    cmd.CommandText = "SELECT UserId, ProductId, Text FROM [Review] WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", Id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Review review = new Review
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                Text = reader.GetString(reader.GetOrdinal("Text")),

                            };
                            return review;
                        }
                        
                    }
                    
                }
            }
            return null;
        }


        public Review EditReview(Review review)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                SqlTransaction transaction = null;

                try
                {
                    using (SqlCommand checkCmd = conn.CreateCommand())
                    {
                        // Check if the review with the given ID exists
                        checkCmd.CommandText = "SELECT COUNT(*) FROM [Review] WHERE Id = @Id";
                        checkCmd.Parameters.AddWithValue("@Id", review.Id);

                        int reviewCount = (int)checkCmd.ExecuteScalar();

                        if (reviewCount == 0)
                        {
                            Console.WriteLine("Review not found.");
                            return null;
                        }
                    }

                    // Begin a transaction to ensure atomicity
                    transaction = conn.BeginTransaction();

                    // Update the review
                    using (SqlCommand updateCmd = new SqlCommand("UPDATE [Review] SET UserId = @UserId, ProductId = @ProductId, [Text] = @Text WHERE Id = @ReviewId", conn, transaction))
                    {
                        updateCmd.Parameters.AddWithValue("@ReviewId", review.Id);
                        updateCmd.Parameters.AddWithValue("@UserId", review.UserId);
                        updateCmd.Parameters.AddWithValue("@ProductId", review.ProductId);
                        updateCmd.Parameters.AddWithValue("@Text", review.Text);

                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Review updated successfully.");
                            // Commit the transaction if update is successful
                            transaction.Commit();
                            return review;
                        }
                        else
                        {
                            Console.WriteLine("Failed to update review.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
            return null;
        }


        ////public Review DeleteReview(string id)
        ////{
        ////    using (SqlConnection conn = Connection)
        ////    {
        // conn.Open();

        ////        using (SqlCommand cmd = conn.CreateCommand())
        ////        {
        ////            cmd.CommandText = "DELETE FROM [Review] WHERE ID = @ID";
        ////            cmd.Parameters.AddWithValue("@ID", id);

        ////            cmd.ExecuteNonQuery();
        ////        }
        ////        return null;
        ////    }

        ////}







    }
}

















































