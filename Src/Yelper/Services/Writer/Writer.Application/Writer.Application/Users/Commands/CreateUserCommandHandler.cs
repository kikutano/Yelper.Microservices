using MediatR;
using Writer.Application.Common.Persistence;
using Writer.Domain.AggregatesModel.UsersAggregate;

namespace Writer.Application.Users.Commands;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
	private readonly WriterDbContext _dbContext;

	public CreateUserCommandHandler(WriterDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		var user = User.Create(request.Name, request.At, request.AvatarUrl);

		await _dbContext.Users.AddAsync(user.Value, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}
