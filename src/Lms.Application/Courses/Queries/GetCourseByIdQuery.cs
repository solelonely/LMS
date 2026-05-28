using Lms.Core.Entities;
using Lms.Core.Interfaces;
using Lms.Core.SeedWork;
using MediatR;

namespace Lms.Application.Courses.Queries;

public record CourseDto(Guid Id, Guid TenantId, string CourseCode, string CourseName, string? Description);

public class GetCourseByIdQuery : IRequest<Result<CourseDto>>
{
    public Guid Id { get; set; }
}

public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Result<CourseDto>>
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseByIdQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (course == null)
        {
            return Result<CourseDto>.Failure($"Course with Id '{request.Id}' not found.");
        }

        var dto = new CourseDto(course.Id, course.TenantId, course.CourseCode, course.CourseName, course.Description);
        return Result<CourseDto>.Success(dto);
    }
}
