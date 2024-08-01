using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Interfaces;
using Project1.Models;

namespace Project1.Repository
{
    public class JobsRepository : IJobsRepository
    {
        private readonly RegisterAPIDbContext _context;
        private readonly ILogger<JobsRepository> _logger;

        public JobsRepository(RegisterAPIDbContext context, ILogger<JobsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            _logger.LogInformation("Getting all the jobs successfully.");
            return await _context.Jobs.ToListAsync();
        }

        // public JobsRepository(RegisterAPIDbContext regiterDbContext)
        // {
        // this.regiterDbContext = regiterDbContext;
        // }

        //public async Task<IEnumerable<Jobs>> GetJobs()
        //{
        //  return await regiterDbContext.Jobs.ToListAsync();
        //}

        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                throw new NullReferenceException("Sorry, no job found with this id " + id);
            }
            else
            {
                return job;
            }
        }


        // public async Task<Jobs> GetJob(int jobId)
        //{
        //  return await regiterDbContext.Jobs.FirstOrDefaultAsync(e => e.JobID == jobId);
        //}
        // public async Task<Jobs> AddJob(Jobs job)
        //{
        //await _context.Jobs.AddAsync(job);
        // await _context.SaveChangesAsync();
        // var result = await regiterDbContext.Jobs.AddAsync(job);
        // await regiterDbContext.SaveChangesAsync();
        // return result.Entity;
        // }


        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Job created successfully.");

            return job;
        }

        // public async Task<Users> AddUser(Users user)
        //{
        //  var result = await regiterDbContext.User.AddAsync(user);
        //await regiterDbContext.SaveChangesAsync();
        //return result.Entity;
        //}
        public async Task<Job> UpdateJob(Job job)
        {
            var result = await _context.Jobs
            .FirstOrDefaultAsync(e => e.Id == job.Id);
            if (result != null)
            {
                result.JobTitle = job.JobTitle;
                result.Noofposts = job.Noofposts;
                result.JobDescription = job.JobDescription;
                result.Experience = job.Experience;
                result.Specialisation = job.Specialisation;
                result.LastDateToApply = job.LastDateToApply;
                result.Salary = job.Salary;
                result.JobType = job.JobType;

                result.Company = job.Company;

                //result.CompanyName = job.CompanyName;
                //result.CompanyLogo = job.CompanyLogo;
                //result.CompanyWebsite = job.CompanyWebsite;
                //result.ContactEmail = job.ContactEmail;
                //result.CompanyCity = job.CompanyCity;
                //result.CompanyState = job.CompanyState;
                //result.CompanyCountry = job.CompanyCountry;
                result.DateCreated = job.DateCreated;
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        // public async Task DeleteJob(int jobId)
        //{
        //  var result = await regiterDbContext.Jobs
        //    .FirstOrDefaultAsync(e => e.JobID == jobId);
        //if (result != null)
        //{
        //  regiterDbContext.Jobs.Remove(result);
        //await regiterDbContext.SaveChangesAsync();
        //}
        //}
        public async Task<ActionResult<Job>> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                throw new NullReferenceException("Sorry, no job found with this id " + id);
            }
            else
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Job deleted successfully.");

                return job;
            }
        }

        public async Task ApplyJob(AppliedJobVM job)
        {
            //var jobs = _context.Jobs.Where(r => r.Id == int.Parse(job.JobId)).First();
            ////var company = _context.Company.Where(r => jobs.Select(r => r.Company.Id).Contains(r.Id)).First();

            //var user = _context.User.Where(r => r.Id == int.Parse(job.UserId)).First();

            //var company = _context.Jobs.Where(r => r.Id == jobs.Id).Select(r => r.Company).First();
            //jobs.Company = company;

            var x = new AppliedJobs { JobRefId = int.Parse(job.JobId), UserRefId = int.Parse(job.UserId) };
            _context.AppliedJobs.AddAsync(x);

            //_context.Entry(x.Job).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();

        }

        //Task<Jobs> IJobsRepository.DeleteJob(int JobID)
        //{
        //  throw new System.NotImplementedException();
        //}
        public bool JobsExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }

        public Task<Job> UpdateJob(User user)
        {
            throw new NotImplementedException();
        }
    }
}