using Lms.Core.SeedWork;

namespace Lms.Core.Entities;

public class User : Entity, IAggregateRoot
{
    public Guid TenantId { get; private set; }
    public string O365ObjectId { get; private set; }
    public string Email { get; private set; }
    public string Name { get; private set; }

    private User()
    {
        O365ObjectId = default!;
        Email = default!;
        Name = default!;
    }

    public User(Guid tenantId, string o365ObjectId, string email, string name)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        O365ObjectId = o365ObjectId;
        Email = email;
        Name = name;
    }
}
