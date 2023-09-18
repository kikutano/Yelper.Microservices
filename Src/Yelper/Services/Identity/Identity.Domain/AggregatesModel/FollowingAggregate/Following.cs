using DomainDrivenDesign;
using ErrorOr;
using Identity.Domain.AggregatesModel.UserAggregate;

namespace Identity.Domain.AggregatesModel.FollowingAggregate;

public class Following : Entity, IAggregateRoot
{
    public virtual Guid UserId { get; private set; }
    public virtual User User { get; private set; } = null!;
    public virtual Guid FollowingUserId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected Following(Guid userId, Guid followingUserId)
    {
        UserId = userId;
        FollowingUserId = followingUserId;
        CreatedAt = DateTime.UtcNow;
    }

    public static ErrorOr<Following> Create(Guid userId, Guid followingUserId)
    {
        return new Following(userId, followingUserId);
    }
}
