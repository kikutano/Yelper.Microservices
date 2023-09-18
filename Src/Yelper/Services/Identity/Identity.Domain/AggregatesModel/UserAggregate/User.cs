using DomainDrivenDesign;
using ErrorOr;

namespace Identity.Domain.AggregatesModel.UserAggregate;

public class User : Entity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string At { get; private set; } = string.Empty;
    public string AvatarUrl { get; private set; } = string.Empty;
    public string Bio { get; private set; } = "I'm new on Yelper! :)";
    public DateTime SubscriptionAt { get; private set; }

    protected User(string name, string at)
    {
        Id = Guid.NewGuid();
        Name = name;
        At = at;
        SubscriptionAt = DateTime.UtcNow;
    }

    public static ErrorOr<User> Create(string name, string at)
    {
        var errors = Validate(name, at);

        if (errors.Any())
        {
            return errors.First();
        }

        return new User(name, at);
    }

    private static List<Error> Validate(string name, string at)
    {
        var errors = new List<Error>();

        if (string.IsNullOrEmpty(name))
        {
            errors.Add(Error.Validation(description: $"name can not be null or empty!"));
        }

        if (string.IsNullOrEmpty(at))
        {
            errors.Add(Error.Validation(description: $"name can not be null or empty!"));
        }

        return errors;
    }
}
