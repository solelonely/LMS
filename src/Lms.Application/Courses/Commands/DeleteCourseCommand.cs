using Lms.Core.SeedWork;
using Lms.Core.Interfaces;
using MediatR;

namespace Lms.Application.Courses.Commands;

public class DeleteCourseCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result>
{
    private readonly ICourseRepository _courseRepository;

    public DeleteCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (course == null)
        {
            return Result.Failure($"Course with Id '{request.Id}' not found.");
        }

        await _courseRepository.DeleteAsync(course, cancellationToken);

        return Result.Success();
    }
}
