using H_Sports.Interfaces;
using Microsoft.AspNetCore.Mvc;
using H_Sports.Models;
using System;
using System.Collections.Generic;

namespace H_Sports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly ISportsRepository _sportsRepository;

        public SportController(ISportsRepository sportsRepository)
        {
            _sportsRepository = sportsRepository ?? throw new ArgumentNullException(nameof(sportsRepository));
        }

        [HttpGet(Name = "GetSport")]
        public ActionResult<IEnumerable<Sport>> Get()
        {
            try
            {
                var sports = _sportsRepository.GetSports();

                if (sports == null || sports.Count == 0)
                {
                    return NoContent();
                }

                return Ok(sports);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
