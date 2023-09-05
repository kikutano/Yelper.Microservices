using ErrorOr;

namespace Identity.Domain.AggregatesModel.UserAggregate;

public static partial class Errors
{
    public class User
    {
        public static Error IdentifierDuplicate(string value) => Error.Conflict(
            code: "User.IdentifierAlreadyUsed",
            description: $"The identifier {value} is already used!");

        public static Error UserNameNotFound(string value) => Error.NotFound(
            code: "Security.UserNameNotFound",
            description: $"The identifier {value} is not found!");
    }
}
