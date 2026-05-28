using Lms.Core.SeedWork;

namespace Lms.Core.Entities;

public class Course : Entity, IAggregateRoot
{
    public Guid TenantId { get; private set; }
    public string CourseCode { get; private set; }
    public string CourseName { get; private set; }
    public string? Description { get; private set; }

    private Course()
    {
        CourseCode = default!;
        CourseName = default!;
    }

    public Course(Guid tenantId, string courseCode, string courseName, string? description = null)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        CourseCode = courseCode;
        CourseName = courseName;
        Description = description;
    }
}
