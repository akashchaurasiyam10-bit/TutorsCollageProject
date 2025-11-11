using Infra.Library;
using Infra.Library.Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Service.Library;

namespace TutorsCollageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private readonly TutorService _tutorservice;
        public TutorController(TutorService tutorService)
        {
            _tutorservice = tutorService;
        }

        [HttpGet("DisplayAllTutors")]
        public async Task<ActionResult<List<Tutor>>> GetAllTutor()
        {
            var tutors = await _tutorservice.GetTutors();
            return Ok(tutors);
        }
        [HttpGet("GetByIdTutor/{id}")]
        public async Task<ActionResult<Tutor>> GetTutor(int id)
        {
            try
            {
                var tutor = await _tutorservice.GetTutor(id);
                if (tutor == null)
                {
                    return NotFound();
                }
                return Ok(tutor);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("AddTutorRecord")]
        public async Task<ActionResult<Tutor>> PostTutor([FromBody] Tutor tutor)
        {
            try
            {
                if (tutor == null)
                {
                    return NotFound();
                }
                await _tutorservice.AddTutors(tutor); // Service method to add student
                                                      // Return success message in Swagger response
                return Ok(new
                {
                    message = "✅ Data Successfully Added",
                    TutorName = tutor.FirstName + " " + tutor.LastName,
                    TutorId = tutor.TutorId
                });
            }
            catch (Exception ex)
            {
                // Return error message in Swagger response
                return BadRequest(new
                {
                    message = "❌ Failed to Add Student",
                    error = ex.Message
                });
            }
        }

        [HttpPut("UpdateTutor")]
        public async Task<ActionResult<Tutor>> PutTutor(int id, Tutor tutor)
        {
            try
            {
                var updateTu = await _tutorservice.UpdateTutors(id, tutor);
                return Ok(new
                {
                    message = "✅ Data Successfully Updated",
                    TutorName = tutor.FirstName + " " + tutor.LastName,
                    TutorId = tutor.TutorId
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Return error message in Swagger response
                return BadRequest(new
                {
                    message = "❌ Failed to Add Student",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("DeleteTutorRecord/{id}")]
        public async Task<ActionResult<Tutor>> Deletetutor(int id)
        {
            try
            {
                var deleted = await _tutorservice.DeleteTutors(id);
                return Ok(new
                {
                    message = "✅ Data Successfully Delete",
                    Deleted__TutorId = id
                });
            }
            catch (InvalidOperationException ex)
            {
                // Return error message in Swagger response
                return BadRequest(new
                {
                    message = "❌ Failed to Add Student",
                    error = ex.Message
                });
            }
        }
    }
}
