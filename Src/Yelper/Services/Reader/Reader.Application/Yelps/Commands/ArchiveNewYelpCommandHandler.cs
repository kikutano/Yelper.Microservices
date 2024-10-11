using MediatR;
using MongoDB.Driver;
using Reader.Application.Yelps.Common;

namespace Reader.Application.Yelps.Commands;

public class ArchiveNewYelpCommandHandler : IRequestHandler<ArchiveNewYelpCommand>
{
	private readonly IMongoDatabase _database;

	public ArchiveNewYelpCommandHandler(IMongoDatabase database)
	{
		_database = database;
	}

	public async Task Handle(ArchiveNewYelpCommand request, CancellationToken cancellationToken)
	{
		await _database.GetCollection<YelpItem>("YelperItem")
			.InsertOneAsync(new YelpItem(
				request.Id,
				request.UserId,
				request.Content,
				request.CreatedAt));
	}
}
