using Microsoft.AspNetCore.Mvc;
using H_Sports.Interfaces;
using H_Sports.Models;

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

        //// GET: api/Review/5
        //[HttpGet("GetReviewById{id}")]
        //public ActionResult<Review> Get(string id)
        //{
        //    var review = _reviewRepository.GetReviewById(id);

        //    if (review == null)
        //    {
        //        return NotFound(); 
        //    }

        //    return Ok(review);
        //}

        // POST: api/Review
        [HttpPost("CreateReview/{UserID}/{ProductID}/{Text}")]
        public IActionResult CreateReview(int UserID, int ProductID, string Text)
        {
            try
            {
                // Create the review
                var review = new Review
                {
                    UserId = UserID,
                    ProductID = ProductID,
                    Text = Text
                };

                // Save the review to the repository 
                _reviewRepo.CreateReview(review);

                // Retrieve the saved review by ID
                var newReview = _reviewRepo.GetReviewById(review.Id); 

                // Return the saved review if not null, otherwise BadRequest
                if (newReview != null)
                {
                    return Ok(newReview);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            { 

                // Return a generic error response
                return StatusCode(500, "Internal server error");

            }
        }


        //// PUT: api/Review/5
        //[HttpPut("Edit Review{Edit Review}")]
        //public ActionResult Put(string id, [FromBody] Review review)
        //{
        //    var existingReview = _reviewRepository.GetReviewById(id);

        //    if (existingReview == null)
        //    {
        //        return NotFound(); 
        //    }

        //    review.ID = existingReview.ID; // Ensure the new review has the correct id
        //    _reviewRepository.EditReview(review);

        //    return NoContent(); 
        //}

        //// DELETE: api/Review/5
        //[HttpDelete("Delete Review by ID{ID}")]
        //public ActionResult Delete(string id)
        //{
        //    var existingReview = _reviewRepository.GetReviewById(id);

        //    if (existingReview == null)
        //    {
        //        return NotFound(); 
        //    }

        //    _reviewRepository.DeleteReview(id);

        //    return NoContent(); 
        //}
    }
}
