namespace Lms.Core.SeedWork;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
