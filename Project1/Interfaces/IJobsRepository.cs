using Microsoft.AspNetCore.Mvc;
using Project1.Models;

namespace Project1.Interfaces
{
    public interface IJobsRepository
    {
        Task<ActionResult<IEnumerable<Job>>> GetJobs();
        Task<ActionResult<Job>> GetJob(int id);
        Task<ActionResult<Job>> PostJob(Job job);
        Task<Job> UpdateJob(User user);
        Task<ActionResult<Job>> DeleteJob(int id);
        bool JobsExists(int id);

        Task ApplyJob(AppliedJobVM job);
    }
}
