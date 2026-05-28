using Lms.Core.Entities;
using Lms.Core.Interfaces;
using Lms.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lms.Infrastructure.Repositories;

public class CourseRepository : EfRepository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Course?> GetByCourseCodeAsync(string courseCode, CancellationToken cancellationToken = default)
    {
        return await DbContext.Courses
            .FirstOrDefaultAsync(c => c.CourseCode == courseCode, cancellationToken);
    }
}
