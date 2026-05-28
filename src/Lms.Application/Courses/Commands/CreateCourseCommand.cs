using Lms.Core.SeedWork;
using MediatR;

namespace Lms.Application.Courses.Commands;

public class CreateCourseCommand : IRequest<Result<Guid>>
{
    public Guid TenantId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<Guid>>
{
    // Normally inject IRepository<Course> here.
    // For demonstration, we just return Success with a dummy Guid.
    public Task<Result<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var newCourseId = Guid.NewGuid();
        return Task.FromResult(Result<Guid>.Success(newCourseId));
    }
}
