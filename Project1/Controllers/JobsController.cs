using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project1.Interfaces;
using Project1.Models;


namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly RegisterAPIDbContext _context;
        //private readonly IMailSender mailSender;
       // private readonly IEmailStructureBuilder emailStructure;
        private readonly IJobsRepository repository;

        public JobsController(RegisterAPIDbContext context, IJobsRepository repository)
        {
            _context = context;
           // this.emailStructure = emailStructure;
            this.repository = repository;
        }

        /*public JobsController(RegisterAPIDbContext context, IJobsRepository jobsRepository,
           ILogger<JobsController> logger)
        {
            _context = context;
            _jobsRepository = jobsRepository;
            _logger = logger;

        }*/
        //public JobsController(IJobsRepository repository)
        //{
        //  _repository = repository;
        //}
        // GET: api/Jobs


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> Jobs()
        {
            //emailStructure.SetMailBody("test user", MailType.Activation);
           // emailStructure.mailToAddress = "orugantiabhinav5@gmail.com";

           // var _mailSender = new MailSender(emailStructure);

            //_mailSender.sendMail();
            return await _context.Jobs.ToListAsync();
        }
        
        [HttpGet("jobTitle/{jobTitle}")]
        public async Task<ActionResult<List<Job>>> GetJobByTitle(string jobTitle)
        {
            var job = await _context.Jobs.Where(j=>j.JobTitle.ToLower().Contains(jobTitle.ToLower())).ToListAsync();

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }


        // GET: api/Jobs/5
        /* [HttpGet("{id}")]
         public async Task<ActionResult<Jobs>> GetJob(int id)
         {
             try
             {
                 return await _jobsRepository.GetJob(id);
             }
             catch (Exception ex)
             {
                 _logger.LogError(ex.Message);
                 return NotFound();
             }
         }*/
        // PUT: api/Jobs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.


        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, Job jobs)
        {
            if (id != jobs.Id)
            {
                return BadRequest();
            }
            _context.Entry(jobs).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }


        [HttpPost]
        [Route("ApplyForAJob")]
        public async Task<ActionResult<AppliedJobs>> ApplyForAJob([FromBody]Object job)
        {
            return Ok(job);
        }


        [HttpPost]
        [Route("apply")]
        public async Task<ActionResult<AppliedJobs>> apply([FromBody] AppliedJobVM job)
        {
            await repository.ApplyJob(job);
            return Ok(JsonConvert.DeserializeObject<AppliedJobVM>(JsonConvert.SerializeObject(job)));
        }

        // POST: api/Jobs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /* [HttpPost]
         public async Task<ActionResult<Jobs>> PostJobs(Jobs jobs)
         {
             await _jobsRepository.AddJob(jobs);

             return CreatedAtAction("GetJob", new { id = jobs.JobID }, jobs);
         }*/

        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }

        // [HttpPost]
        //public async Task<ActionResult<Users>> PostUsers(Users users)
        // {
        // _context.Registration.Add(registration);
        //await _context.SaveChangesAsync();
        // await _userRepository.AddUser(users);

        // return CreatedAtAction("GetUser", new { id = users.UserID }, users);
        // }
        //private object GetJob()
        // {
        //   throw new NotImplementedException();
        // }

        // DELETE: api/Jobs/5
        /* [HttpDelete("{id}")]
         public async Task<ActionResult<Jobs>> DeleteJobs(int id)
         {
             try
             {
                 return await _jobsRepository.DeleteJob(id);
             }
             catch (Exception ex)
             {
                 _logger.LogError(ex.Message);
                 return NotFound();
             }
         }*/

        [HttpDelete("{id}")]
        public async Task<ActionResult<Job>> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return job;
        }

        private bool JobsExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}

