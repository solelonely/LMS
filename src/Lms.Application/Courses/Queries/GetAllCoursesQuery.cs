using Lms.Core.Interfaces;
using Lms.Core.SeedWork;
using MediatR;

namespace Lms.Application.Courses.Queries;

public class GetAllCoursesQuery : IRequest<Result<IReadOnlyList<CourseDto>>>
{
}

public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, Result<IReadOnlyList<CourseDto>>>
{
    private readonly ICourseRepository _courseRepository;

    public GetAllCoursesQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<IReadOnlyList<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.ListAllAsync(cancellationToken);

        var dtos = courses.Select(c => new CourseDto(c.Id, c.TenantId, c.CourseCode, c.CourseName, c.Description)).ToList();

        return Result<IReadOnlyList<CourseDto>>.Success(dtos.AsReadOnly());
    }
}
