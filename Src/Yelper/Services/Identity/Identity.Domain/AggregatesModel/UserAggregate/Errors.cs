using ErrorOr;

namespace Identity.Domain.AggregatesModel.UserAggregate;

public static partial class Errors
{
    public class User
    {
        public static Error AtDuplicate(string value) => Error.Conflict(
            code: "User.AtAlreadyUsed",
            description: $"The At {value} is already used!");

        public static Error AtNotFound(string value) => Error.NotFound(
            code: "User.AtNotFound",
            description: $"The At {value} is not found!");

        public static Error UserIdNotFound(Guid value) => Error.NotFound(
            code: "User.UserId",
            description: $"The userId {value} is not found!");
    }
}
