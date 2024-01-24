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
                    cmd.CommandText = "SELECT UserId, ProductID, Text FROM [Review]";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var reviews = new List<Review>();
                        while (reader.Read())
                        {
                            Review review = new Review
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                                Text = reader.GetString(reader.GetOrdinal("Text")),
                            };
                            reviews.Add(review);
                        }
                        return reviews;
                    }
                }
            }
        }
        public Review CreateReview(Review review)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [Review] (UserId, ProductID, Text) VALUES (@UserId, @ProductID, @Text)";
                    cmd.Parameters.AddWithValue("@UserId", review.UserId);
                    cmd.Parameters.AddWithValue("@ProductID", review.ProductID);
                    cmd.Parameters.AddWithValue("@Text", review.Text);
                }
                return review;
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
                                ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
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

//        public void DeleteReview(string id)
//        {
//            using (SqlConnection conn = Connection)
//            {
//                conn.Open();

//                using (SqlCommand cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = "DELETE FROM [Review] WHERE ID = @ID";
//                    cmd.Parameters.AddWithValue("@ID", id);  // Assuming 'id' is the correct parameter

//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }
 }
}

