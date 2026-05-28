using Lms.Core.Entities;

namespace Lms.Core.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    // Add any course-specific repository methods here, for example:
    Task<Course?> GetByCourseCodeAsync(string courseCode, CancellationToken cancellationToken = default);
}
