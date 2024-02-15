using ErrorOr;

namespace Writer.Domain.AggregatesModel.UsersAggregate;

public static partial class Errors
{
    public class User
    {
        public static Error UserIdNotFound(Guid value) => Error.NotFound(
            code: "User.UserId",
            description: $"The userId {value} is not found!");
    }
}
