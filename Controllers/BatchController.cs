using Infra.Library;
using Infra.Library.Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Library;

namespace TutorsCollageProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly BatchService _batchService;
        public BatchController(BatchService batchService)
        {
            _batchService = batchService;
        }
        #region Display one And All
        [HttpGet("DisplayAllBatch")]
        public async Task<ActionResult<List<Batch>>> GetAll()
        {
            return await _batchService.GetAllBatch();
        }
        [HttpGet("GetByIdBatch/{id}")]
        public async Task<ActionResult<Batch>> GetBatch(int id)
        {
            try
            {
                var batch = await _batchService.GetBatch(id);
                return Ok(batch);
            }
        
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        #endregion

        #region Add Data
        [HttpPost("AddBatchRecords")]
        public async Task<ActionResult<Batch>> PostBatch([FromBody] Batch batch)
        {
            await _batchService.AddBatch(batch);
            return Ok(new
            {
                message = "✅ Data Successfully Added",
                BatchName = batch.BatchName,
                BatchId = batch.BatchId
            });
        }
        #endregion

        #region Edit & update Data
        [HttpPut("UpdateBatchRecords")]
        public async Task<ActionResult<Batch>> PutBatch(int id,[FromBody]  Batch batch)
        {
            try
            {
                var updated = await _batchService.UpdateBatch(id, batch);
                return Ok(new
                {
                    message = "✅ Data Successfully Updated",
                    BatchName = batch.BatchName,
                    BatchId = batch.BatchId
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

        #region Delete 
        [HttpDelete("DeleteBatchRecords/{id}")]
        public async Task<ActionResult<Batch>>  DeleteBatch(int id)
        {
            try
            {
                var deleted = await _batchService.DeleteBatch(id);
                return Ok(new
                {
                    message = "✅ Data Successfully Delete",
                    Deleted__BatchtId = id
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

        #region Student DropDown List
        [HttpGet("dropdown/students")]
        public async Task<ActionResult<List<object>>> GetStudentDropdown()
        {
            var students = await _batchService.StudentDropDown();
            return Ok(students);
        }
        #endregion

        #region tutor DropDown List
        [HttpGet("dropdown/tutors")]
        public async Task<ActionResult<List<object>>> GetTutorDropDown()
        {
            var tutors = await _batchService.TutorDropDown();
            return Ok(tutors);
        }
        #endregion
    }
}
