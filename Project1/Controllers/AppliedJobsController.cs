using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.Interfaces;
using Project1.Models;
using Project1.Repository;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppliedJobsController : ControllerBase
    {
        private readonly RegisterAPIDbContext _context;
        private readonly IAppliedJobsRepository _repository;
        public AppliedJobsController(IAppliedJobsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/AppliedJobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppliedJobsResponse>>> GetAppliedJobs()
        {
            return await _repository.GetAllAppliedJobs();

        }

        // GET: api/AppliedJobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppliedJobs>> GetAppliedJobs(int id)
        {
            var appliedJobs = await _repository.GetAppliedJobById(id);

            if (appliedJobs == null)
            {
                return NotFound();
            }

            return appliedJobs;
        }
        [HttpGet]
        [Route("AppliedJobsByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<Job>>> GetAppliedJobsByUserId(int id)
        {
            return await _repository.GetAppliedJobsByUserId(id);
        }

        // PUT: api/AppliedJobs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppliedJobs(int id, AppliedJobs appliedJobs)
        {
            if (id != appliedJobs.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAppliedJob(appliedJobs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // POST: api/AppliedJobs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AppliedJobs>> AddAppliedJob(AppliedJobs appliedJob)
        {
            var result = await _repository.AddAppliedJobAsync(appliedJob);
            return CreatedAtAction(nameof(GetAppliedJobById), new { id = result.Id }, result);
        }

        private object GetAppliedJobById()
        {
            throw new NotImplementedException();
        }

        // DELETE: api/AppliedJobs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppliedJobs>> DeleteAppliedJobs(int id)
        {
            try
            {
                await _repository.DeleteAppliedJob(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("upload/{id}")]
        public async Task<string> Upload(int id)
        {
            var file = Request.Form.Files[0];
            return await _repository.UploadResume(id, file);
        }

        private bool AppliedJobsExists(int id)
        {
            return _context.AppliedJobs.Any(e => e.Id == id);
        }
    }
}
