using Microsoft.AspNetCore.Mvc;
using H_Sports.Interfaces;
using H_Sports.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Components.Forms;
using H_Sports.Repositories;
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;

namespace H_Sports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepo = reviewRepository;
        }

        // GET: api/Review
        [HttpGet("GetReviews")]
        public ActionResult<IEnumerable<Review>> Get()
        {
            var reviews = _reviewRepo.GetReviews();
            return Ok(reviews);
        }

        // POST: api/Review
        [HttpPost("CreateReview/{UserID}/{ProductId}/{Text}")]
        public IActionResult CreateReview(int UserID, int ProductId, string Text)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(Text))
                {
                    return BadRequest("Review text cannot be empty");
                }

                // Create the review
                var review = new Review
                {
                    UserId = UserID,
                    ProductId = ProductId,
                    Text = Text
                };

                // Save the review using the repository
                var reviewId = _reviewRepo.CreateReview(review);

                // Check if id exists
                if (reviewId != 0)
                {
                    // Return new user Id
                    var productReview = _reviewRepo.GetReviewById(reviewId);
                    productReview.Id = reviewId;

                    // If so, get review by id and return it
                    return Ok(productReview);
                }
                else
                {
                    return BadRequest();
                }

                // If not, return bad http status
            }
            catch (Exception)
            {
                // Return a generic error response
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Review/DeleteReview/{id}
        [HttpDelete("DeleteReview/{id}")]
        public IActionResult DeleteReview(int id)
        {
            try
            {
                // Check if the review exists
                var existingReview = _reviewRepo.GetReviewById(id);

                if (existingReview == null)
                {
                    return NotFound($"Review with ID {id} not found");
                }

                // Delete the review using the repository
                _reviewRepo.DeleteReview(id);

                // Return a success message
                return Ok($"Review with ID {id} deleted successfully");
            }
            catch (Exception)
            {
                // Return a generic error response
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
