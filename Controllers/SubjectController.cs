using Infra.Library;
using Infra.Library.Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Library;

namespace TutorsCollageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly SubjectService _subjectService;
        public SubjectController(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet("GetAllSubject")]
        public async Task<ActionResult<List<Subject>>> GetAllSub()
        {
            return await _subjectService.GetAll();
        }
        [HttpGet("GetByIdSubject/{id}")]
        public async Task<ActionResult<Subject>> Get(int id)
        {
            try
            {
                var subject = await _subjectService.GetSingle(id);
                return Ok(subject);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPost("AddSubjectRecord")]
        public async Task<ActionResult<Subject>> AddSub(Subject subject)
        {
            try
            {
                await _subjectService.Add(subject);
                return Ok(new
                {
                    message = " Data Successfully Added",
                    SubjectId = subject.SubjectId,
                    SubjectName = subject.SubjectName,
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPut("UpdateSubjectRecords/{id}")]
        public async Task<ActionResult<Subject>> UpdateSub(int id, Subject subject)
        {
            try
            {
                await _subjectService.Update(id, subject);
                return Ok(new
                {
                    message = " Data Successfully Updated",
                    SujectId = subject.SubjectId,
                    UpdatedName = subject.SubjectName,
                    Status = subject.Active
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
        [HttpDelete("DeleteSubjectRecord/{id}")]
        public async Task<ActionResult<Subject>> DeleteSub(int id)
        {
            //  await  _subjectService.Delete(id);
            //return Ok();

            try
            {
                await _subjectService.Delete(id);
                return Ok(new
                {
                    message = " Data Successfully  Deleted",
                    SubjectId = id,
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
