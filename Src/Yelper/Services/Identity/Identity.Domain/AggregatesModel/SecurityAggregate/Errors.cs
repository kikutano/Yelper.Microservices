using ErrorOr;

namespace Identity.Domain.AggregatesModel.SecurityAggregate;

public static partial class Errors
{
    public class Security
    {
        public static Error AccessCodeIsNotCorrect() => Error.Failure(
            code: "Security.AccessCodeIsNotCorrect",
            description: $"The access code is not correct!");
    }
}