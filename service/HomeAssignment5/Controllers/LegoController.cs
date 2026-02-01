using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeAssignment5.BL;
using HomeAssignment5.Repositories;

namespace HomeAssignment5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // כל הפעולות דורשות שהמשתמש יהיה מחובר
    public class LegoController : ControllerBase
    {
        private readonly ILegoRepository repository;

        public LegoController(ILegoRepository repository)
        {
            this.repository = repository; // הזרקת תלויות
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<LegoSet> sets = repository.GetAll();
                return Ok(sets);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving Lego sets: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] LegoSet set)
        {
            if (set == null || string.IsNullOrEmpty(set.Name))
                return BadRequest("Invalid Lego set");
            try
            {
                LegoSet addedSet = repository.Add(set);
                return Ok(addedSet);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error adding Lego set: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = repository.Delete(id);
                if (!deleted)
                    return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error deleting Lego set: " + ex.Message);
            }

        }
    }
}
