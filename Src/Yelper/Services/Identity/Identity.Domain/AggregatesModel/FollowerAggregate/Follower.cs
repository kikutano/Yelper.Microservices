using DomainDrivenDesign;
using ErrorOr;
using Identity.Domain.AggregatesModel.UserAggregate;

namespace Identity.Domain.AggregatesModel.FollowerAggregate;

public class Follower : Entity, IAggregateRoot
{
    public virtual Guid UserId { get; private set; }
    public virtual User User { get; private set; } = null!;
    public virtual Guid FollowedByUserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool Following { get; private set; }

    protected Follower(Guid userId, Guid followedByUserId)
    {
        UserId = userId;
        FollowedByUserId = followedByUserId;
        CreatedAt = DateTime.UtcNow;
        Following = false;
    }

    public static ErrorOr<Follower> Create(Guid userId, Guid followedByUserId)
    {
        return new Follower(userId, followedByUserId);
    }
}