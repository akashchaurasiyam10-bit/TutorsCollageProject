using Infra.Library;
using Microsoft.AspNetCore.Mvc;
using Service.Library;

namespace TutorsCollageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly EnrollmentService _enrollmentService;
        public EnrollmentController(EnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;   // inject to the Service Object
        }

        // Cretae DropDown In Related Student Id & Batch Id

        [HttpGet("dropdown/students")]
        public async Task<ActionResult<List<object>>> GetStudentDropdown()
        {
            var students = await _enrollmentService.GetStudentDropdown();
            return Ok(students);
        }

        [HttpGet("dropdown/batches")]
        public async Task<ActionResult<List<object>>> GetBatchDropdown()
        {
            var batches = await _enrollmentService.GetBatchDropdown();
            return Ok(batches);
        }


        [HttpGet("GetByAllEnroment")]
        public async Task<ActionResult<List<Enrollment>>> GetsAll()
        {
            return await _enrollmentService.GetAll();
        }

        [HttpGet("GetByIdEnrollment/{Id}")]
        public async Task<ActionResult<Enrollment>> GetSingle(int Id)
        {

            try
            {
                var enroll = await _enrollmentService.Get(Id);
                return Ok(enroll);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("AddEnrollment")]
        public async Task<ActionResult<Enrollment>> PostEnrollment([FromBody] Enrollment enrollment)
        {
            try
            {
                await _enrollmentService.Add(enrollment);
                return Ok(new
                {
                    message = "✅ Data Successfully Added",
                    StudentId = enrollment.StudentId,
                    EnrollmentId = enrollment.EnrollmentId
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPut("UpdateEnrollment")]
        public async Task<ActionResult<Enrollment>> PutEnrollment(int Id, Enrollment enrollment)
        {
            try
            {
                await _enrollmentService.Update(Id, enrollment);
                return Ok(new
                {
                    message = "✅ Data Successfully Updated",
                    UpdatedStudentId = enrollment.StudentId,
                    UpdatedBatchId = enrollment.BatchId,
                    UpdatedStatus = enrollment.Status
                });

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpDelete("DeleteEnrollment/{Id}")]
        public async Task<ActionResult<Enrollment>> DeleteEnrollment(int Id)
        {
            try
            {
                await _enrollmentService.Delete(Id);
                return Ok(new
                {
                    message = "✅ Data Successfully  Deleted",
                    EnrollmentId = Id,
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
