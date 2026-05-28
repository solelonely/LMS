using Lms.Core.SeedWork;

namespace Lms.Core.Entities;

public class Tenant : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string? Domain { get; private set; }

    private Tenant() { Name = default!; } // ORM requirement

    public Tenant(string name, string? domain = null)
    {
        Id = Guid.NewGuid();
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException(nameof(name)) : name;
        Domain = domain;
    }
}
