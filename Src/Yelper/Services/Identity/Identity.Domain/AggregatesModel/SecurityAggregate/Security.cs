using DomainDrivenDesign;
using ErrorOr;

namespace Identity.Domain.AggregatesModel.SecurityAggregate;

public class Security : Entity, IAggregateRoot
{
    public string AccessCode { get; private set; } = null!;
    public virtual Guid UserId { get; private set; }

    protected Security(Guid userId)
    {
        UserId = userId;
        AccessCode = GenerateAccessCode();
    }

    public static ErrorOr<Security> Create(Guid userId)
    {
        return new Security(userId);
    }

    private static string GenerateAccessCode()
    {
        return "1234";
    }
}
