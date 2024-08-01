using Microsoft.EntityFrameworkCore;
using Project1.Interfaces;
using Project1.Models;
using System.Net.Http.Headers;

namespace Project1.Repository
{
    public class AppliedJobsRepository : IAppliedJobsRepository
    {
        private readonly RegisterAPIDbContext _context;

        public AppliedJobsRepository(RegisterAPIDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppliedJobsResponse>> GetAllAppliedJobs()
        {
            //var jobs = _context.Jobs.Join(_context.AppliedJobs,
            //        job => job.Id,
            //        applied => applied.JobRefId,
            //        (job, applied) => job);
            //var company = _context.Company.Where()

            var list = new List<AppliedJobsResponse>();

            var search = await _context.AppliedJobs.ToListAsync();

            foreach (var job in search)
            {
                var joblist = await _context.Jobs.FindAsync(job.JobRefId);
                var userlist = await _context.User.FindAsync(job.UserRefId);
                joblist.Company.Name = _context.Company.Where(c => c.Id == joblist.CompanyRefId).First().Name;
                
                var finallist = new AppliedJobsResponse();
                finallist.Id = job.Id;
                finallist.UserName = userlist.FirstName;
                finallist.UserEmail = userlist.Email;
                finallist.PhoneNumber = userlist.Phone;
                finallist.JobTitle = joblist.JobTitle;
                finallist.CompanyName = joblist.Company.Name;


                list.Add(finallist);
            }

            return list;
        }

        public async Task<AppliedJobs> GetAppliedJobById(int appliedJobId)
        {
            return await _context.AppliedJobs.FindAsync(appliedJobId);
        }

        public async Task<List<Job>> GetAppliedJobsByUserId(int userId)
        {
            var jobs = await _context.Jobs.Join(_context.AppliedJobs.Where(a => a.UserRefId == userId),
                    job => job.Id,
                    applied => applied.JobRefId,
                    (job, applied) => job)
                .ToListAsync();

            jobs.ForEach(job =>
            {
                job.Company.Name = _context.Company.Where(c => c.Id == job.CompanyRefId).First().Name;
            });

            //var company = _context.
            return jobs; // await _context.AppliedJobs.Where(a => a.Id == userId).ToListAsync();
        }

        public async Task<AppliedJobs> AddAppliedJobAsync(AppliedJobs appliedJob)
        {
            await _context.AppliedJobs.AddAsync(appliedJob);
            await _context.SaveChangesAsync();
            return appliedJob;
        }

        public async Task UpdateAppliedJob(AppliedJobs appliedJob)
        {
            _context.Entry(appliedJob).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppliedJob(int appliedJobId)
        {
            var appliedJob = await _context.AppliedJobs.FindAsync(appliedJobId);
            _context.AppliedJobs.Remove(appliedJob);
            await _context.SaveChangesAsync();
        }

        public async Task<string> UploadResume(int userId, IFormFile file)
        {
            //var file = Request.Form.Files[0];
            var folderName = Path.Combine("Resources", "Resume");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if(!Directory.Exists(pathToSave)) {
                Directory.CreateDirectory(pathToSave);
            }

            string dbPath = "";

            if (file.Length > 0)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

            }

            _context.User.Where(r => r.Id == userId).First().Resume = dbPath;
            await _context.SaveChangesAsync();
            return dbPath;

        }
    }
}
