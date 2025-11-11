using Infra.Library;
using Infra.Library.Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Library;

namespace TutorsCollageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        // Implement Services Class In Controller and Create Services Objects

        private readonly StudentService _studentService;
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }


        #region Display ALL & Fetch  Single Records
        [HttpGet("DisplayAllStudent")]
        public async Task<ActionResult<List<Student>>> GetAll()
        {
            var students = await _studentService.GetAllAsync();  // GetAllAsync Method Call in Services Class
            return Ok(students);
        }

        [HttpGet("GetByIdStudent/{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            try
            {
                var student = await _studentService.GetById(id); //  // GetById Method Call in Services Class
                return Ok(student);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }

            //var student = await _studentService.GetById(id);
            //if (student == null)
            //{
            //    return NotFound();
            //}
            //return student;

        }
        #endregion

        #region Add Student 
        [HttpPost("AddStudentRecord")]
        public async Task<ActionResult<Student>> PostStudent([FromBody] Student student)
        {

            //if(student == null)
            //{
            //    return NotFound();
            //}
            //await _studentService.AddStudent(student);
            //return Ok(student);

            var added = await _studentService.AddStudent(student);   // // AddStudent Method Call in Services Class
            return Ok(new
            {
                message = "✅ Data Successfully Added",
                StudentName = student.FirstName + " " + student.LastName,
                StudentId = student.StudentId
            });
            //return CreatedAtAction(nameof(Get), new { id = added.StudentId }, added);
        }
        #endregion

        #region Edit And Update Student Records

        [HttpPut("UpdateStudent")]
        public async Task<ActionResult<Student>> PutStudent(int id, Student student)
        {
            try
            {
                var updated = await _studentService.UpdateStudent(id, student);
                return Ok(new
                {
                    message = "✅ Data Successfully Updated",
                    StudentName = student.FirstName + " " + student.LastName,
                    StudentId = student.StudentId
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
        #endregion

        #region Delete Records 

        [HttpDelete("DeleteStudentRecord/{id}")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            try
            {
                var deleted = await _studentService.DeleteStudent(id);
                return Ok(new
                {
                    message = "✅ Data Successfully Delete",
                    Deleted__StudentId = id
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

        #endregion



    }
}
