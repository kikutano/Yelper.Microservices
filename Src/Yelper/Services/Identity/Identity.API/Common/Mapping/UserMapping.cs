using Identity.Application.Users.Common;
using Identity.Contracts.Users;
using Identity.Domain.AggregatesModel.UserAggregate;
using Mapster;

namespace Identity.API.Common.Mapping;

public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>()
            .Map(dest => dest, src => src);
        config.NewConfig<IEnumerable<User>, UsersDto>()
            .Map(dest => dest.Users, src => src);
        config.NewConfig<UserCreatedResult, UserCreatedDto>()
            .Map(dest => dest.UserId, src => src.User.Id)
            .Map(dest => dest.AccessCode, src => src.Security.AccessCode)
            .Map(dest => dest.Identifier, src => src.User.Identifier)
            .Map(dest => dest.Name, src => src.User.Name);
    }
}
