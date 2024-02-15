using DomainDrivenDesign;
using ErrorOr;

namespace Writer.Domain.AggregatesModel.UsersAggregate;

public class User : Entity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string At { get; private set; } = string.Empty;
    public string AvatarUrl { get; private set; } = string.Empty;

    protected User(string name, string at, string avatarUrl)
    {
        Name = name;
        At = at;
        AvatarUrl = avatarUrl;
    }

    public static ErrorOr<User> Create(string name, string at, string avatarUrl)
    {
        var errors = Validate(name, at);

        if (errors.Any())
        {
            return errors.First();
        }

        return new User(name, at, avatarUrl);
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
