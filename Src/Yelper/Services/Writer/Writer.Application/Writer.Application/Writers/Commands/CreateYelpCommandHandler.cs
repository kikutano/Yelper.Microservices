using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Writer.Application.Common.Persistence;
using Writer.Domain.AggregatesModel.UsersAggregate;
using Writer.Domain.AggregatesModel.YelpsAggregate;

namespace Writer.Application.Writers.Commands;

public sealed class CreateYelpCommandHandler : IRequestHandler<CreateYelpCommand, ErrorOr<CreateYelpResult>>
{
	private readonly WriterDbContext _dbContext;

	public CreateYelpCommandHandler(WriterDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<ErrorOr<CreateYelpResult>> Handle(
		CreateYelpCommand request, CancellationToken cancellationToken)
	{
		var userExist = await _dbContext
			.Users
			.AnyAsync(x => x.Id == request.UserId);

		if (!userExist)
		{
			return Errors.User.UserIdNotFound(request.UserId);
		}

		var yelp = Yelp.Create(request.UserId, request.Text);

		if (yelp.IsError)
		{
			return yelp.Errors.FirstOrDefault();
		}

		await _dbContext.Yelps.AddAsync(yelp.Value, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);

		return new CreateYelpResult(yelp.Value.Id, yelp.Value.CreatedAt);
	}
}
