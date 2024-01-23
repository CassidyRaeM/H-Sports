using H_Sports.Interfaces;
using Microsoft.AspNetCore.Mvc;
using H_Sports.Models;
using H_Sports.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace H_Sports.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        // GET: api/<UserController>
        //[HttpGet]
        //public IActionResult GetAllUsers()
        //{

        //    return Ok(_userRepo.GetUsers());
        //}

        //// GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<UserController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        // GET: api/<UserController>
        [HttpGet("GetUserByUserName/{username}")]
        public IActionResult GetUserByUserName(string username)
        {
            var user = _userRepo.GetUserByUserName(username);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }


        //POST: Create a new User 
        [HttpPost("CreateUser{UserName},{Email}, {FirstName},{LastName}")]
        public IActionResult CreateUser([FromBody] User newUser)
        {

           
            if (newUser == null)
            {
                return BadRequest("Invalid user data");
            }

            try
            {
                // Use the CreateUser method from the repository
                _userRepo.CreateUser(newUser);

                
                return CreatedAtAction(nameof(GetUserByUserName), new { userName = newUser.UserName }, newUser);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
