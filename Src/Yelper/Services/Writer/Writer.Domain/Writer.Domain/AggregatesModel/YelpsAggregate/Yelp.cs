using DomainDrivenDesign;
using ErrorOr;

namespace Writer.Domain.AggregatesModel.YelpsAggregate;

public class Yelp : Entity, IAggregateRoot
{
    public Guid UserId { get; private set; }
    public string Text { get; private set; } = string.Empty;

    protected Yelp(Guid userId, string text)
    {
        UserId = userId;
        Text = text;
    }

    public static ErrorOr<Yelp> Create(Guid userId, string text)
    {
        var errors = Validate(text);

        if (errors.Any())
        {
            return errors.First();
        }

        return new Yelp(userId, text);
    }

    public static List<Error> Validate(string text)
    {
        var errors = new List<Error>();

        if (string.IsNullOrEmpty(text))
        {
            errors.Add(Error.Validation(description: $"text can not be null or empty!"));
        }

        return errors;
    }
}
