using Project1.Models;

namespace Project1.Interfaces
{
    public interface IAppliedJobsRepository
    {
        Task<List<AppliedJobsResponse>> GetAllAppliedJobs();
        Task<AppliedJobs> GetAppliedJobById(int appliedJobId);
        Task<List<Job>> GetAppliedJobsByUserId(int userId);
        Task<AppliedJobs> AddAppliedJobAsync(AppliedJobs appliedJob);
        //Task AddAppliedJob(AppliedJobs appliedJob);
        Task UpdateAppliedJob(AppliedJobs appliedJob);
        Task DeleteAppliedJob(int appliedJobId);

        Task<string> UploadResume(int userId, IFormFile file);
    }
}
