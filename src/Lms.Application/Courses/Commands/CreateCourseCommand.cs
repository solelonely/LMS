using Lms.Core.Entities;
using Lms.Core.Interfaces;
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
    private readonly ICourseRepository _courseRepository;

    public CreateCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        // Check if course code already exists
        var existingCourse = await _courseRepository.GetByCourseCodeAsync(request.CourseCode, cancellationToken);
        if (existingCourse != null)
        {
            return Result<Guid>.Failure($"Course with code '{request.CourseCode}' already exists.");
        }

        var course = new Course(request.TenantId, request.CourseCode, request.CourseName, request.Description);

        await _courseRepository.AddAsync(course, cancellationToken);

        return Result<Guid>.Success(course.Id);
    }
}
