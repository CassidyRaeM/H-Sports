using H_Sports.Interfaces;
using H_Sports.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Connections;


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
                    cmd.CommandText = "SELECT UserId, ProductID, Text, Id FROM [Review]";
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
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            };
                            reviews.Add(review);
                        }
                        return reviews;
                    }
                }
            }
        }
        public int CreateReview(Review review)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO [Review] (UserId, ProductId, Text) 
                                OUTPUT INSERTED.ID
                                VALUES (@UserId, @ProductID, @Text)";
                        cmd.Parameters.AddWithValue("@UserId", review.UserId);
                        cmd.Parameters.AddWithValue("@ProductID", review.ProductId);
                        cmd.Parameters.AddWithValue("@Text", review.Text);

                        object result = cmd.ExecuteScalar();

                        int Id = Convert.ToInt32(result);
                        return Id;
                    }
                }
                Console.WriteLine("Review added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }




        public Review GetReviewById(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())

                {
                    cmd.CommandText = "SELECT UserId, ProductID, Text FROM [Review] WHERE Id = @Id";
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
                    return null;
                }
            }
        }




        //        public void EditReview(Review review)
        //        {
        //            using (SqlConnection conn = Connection)
        //            {
        //                //conn.Open();

        //                using (SqlCommand cmd = conn.CreateCommand())
        ////                {
        ////                    cmd.CommandText = "UPDATE [Review] SET UserId = @UserId, ProductID = @ProductID, Text = @Text WHERE ID = @ID";
        ////                    cmd.Parameters.AddWithValue("@ID", review.UserId);  
        ////                    cmd.Parameters.AddWithValue("@UserId", review.UserId);
        ////                    cmd.Parameters.AddWithValue("@ProductID", review.ProductID);
        ////                    cmd.Parameters.AddWithValue("@Text", review.Text);

        ////                    cmd.ExecuteNonQuery();
        ////                }
        //            }
        //        }

        public void DeleteReview(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM [Review] WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", Id);

                    cmd.ExecuteNonQuery();
                }


            }
        }
    }

}
