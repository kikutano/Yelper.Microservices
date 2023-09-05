using DomainDrivenDesign;
using ErrorOr;

namespace Identity.Domain.AggregatesModel.UserAggregate;

public class User : Entity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Identifier { get; private set; } = string.Empty;
    public string AvatarUrl { get; private set; } = string.Empty;
    public string Bio { get; private set; } = "I'm new on Yelper! :)";
    public DateTime SubscriptionAt { get; private set; }

    protected User(string name, string identifier)
    {
        Name = name;
        Identifier = identifier;
        SubscriptionAt = DateTime.UtcNow;
    }

    public static ErrorOr<User> Create(string name, string identifier)
    {
        var errors = Validate(name, identifier);

        if (errors.Any())
        {
            return errors.First();
        }

        return new User(name, identifier);
    }

    private static List<Error> Validate(string name, string identifier)
    {
        var errors = new List<Error>();

        if (string.IsNullOrEmpty(name))
        {
            errors.Add(Error.Validation(description: $"name can not be null or empty!"));
        }

        if (string.IsNullOrEmpty(identifier))
        {
            errors.Add(Error.Validation(description: $"name can not be null or empty!"));
        }

        return errors;
    }
}
