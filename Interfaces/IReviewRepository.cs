using H_Sports.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;



namespace H_Sports.Interfaces
{
    public interface IReviewRepository
    {
        // Retrieve all reviews
         List<Review> GetReviews();

        /// Add a new review
        int CreateReview( Review review);

        //// Retrieve a review by its ID
        Review GetReviewById(int ID);

        //// Delete a review by its ID
        void DeleteReview(int Id);

      


    }
}
