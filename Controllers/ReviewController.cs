using Microsoft.AspNetCore.Mvc;
using H_Sports.Interfaces;
using H_Sports.Models;

namespace H_Sports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // GET: api/Review
        [HttpGet("GetReviews")]
        public ActionResult<IEnumerable<Review>> Get()
        {
            var reviews = _reviewRepository.GetReviews();
            return Ok(reviews);
        }

        // GET: api/Review/5
        [HttpGet("GetReviewById{id}")]
        public ActionResult<Review> Get(string id)
        {
            var review = _reviewRepository.GetReviewById(id);

            if (review == null)
            {
                return NotFound(); 
            }

            return Ok(review);
        }

        // POST: api/Review
        [HttpPost("CreateReview{Usre ID},{ProductID},{Review}")]
        public ActionResult Post([FromBody] Review review)
        {
            _reviewRepository.AddReview(review);
            return CreatedAtAction(nameof(Get), new { ID = review.ID }, review);
        }

        // PUT: api/Review/5
        [HttpPut("Edit Review{Edit Review}")]
        public ActionResult Put(string id, [FromBody] Review review)
        {
            var existingReview = _reviewRepository.GetReviewById(id);

            if (existingReview == null)
            {
                return NotFound(); 
            }

            review.ID = existingReview.ID; // Ensure the new review has the correct id
            _reviewRepository.EditReview(review);

            return NoContent(); 
        }

        // DELETE: api/Review/5
        [HttpDelete("Delete Review by ID{ID}")]
        public ActionResult Delete(string id)
        {
            var existingReview = _reviewRepository.GetReviewById(id);

            if (existingReview == null)
            {
                return NotFound(); 
            }

            _reviewRepository.DeleteReview(id);

            return NoContent(); 
        }
    }
}
