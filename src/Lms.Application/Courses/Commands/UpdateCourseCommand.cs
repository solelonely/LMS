using Lms.Core.SeedWork;
using Lms.Core.Interfaces;
using MediatR;

namespace Lms.Application.Courses.Commands;

public class UpdateCourseCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Result>
{
    private readonly ICourseRepository _courseRepository;

    public UpdateCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (course == null)
        {
            return Result.Failure($"Course with Id '{request.Id}' not found.");
        }

        // Note: CourseCode is read-only per our domain model (set via constructor only)
        // If we need to update name/description, we should ideally add a method to the Course entity:
        // course.UpdateDetails(request.CourseName, request.Description);
        // For simplicity right now, since properties have private setters, we'll need to add an update method to Course.

        course.UpdateDetails(request.CourseName, request.Description);

        await _courseRepository.UpdateAsync(course, cancellationToken);

        return Result.Success();
    }
}
