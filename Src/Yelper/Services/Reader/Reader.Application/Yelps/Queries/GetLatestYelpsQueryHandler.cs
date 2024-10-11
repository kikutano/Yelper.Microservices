using MediatR;
using MongoDB.Driver;
using Reader.Application.Yelps.Common;

namespace Reader.Application.Yelps.Queries;

public class GetLatestYelpsQueryHandler : IRequestHandler<GetLatestYelpsQuery, List<YelpItem>>
{
	private readonly IMongoClient _mongoClient;
	private readonly IMongoDatabase _database;
	private IMongoCollection<YelpItem> _yelpCollection;

	public GetLatestYelpsQueryHandler(IMongoClient mongoClient, IMongoDatabase database)
	{
		_mongoClient = mongoClient;
		_database = database;
	}

	public async Task<List<YelpItem>> Handle(GetLatestYelpsQuery request, CancellationToken cancellationToken)
	{
		//_yelpCollection = _database.GetCollection<YelpItem>("YelpItem");

		//Guid userId = Guid.NewGuid();

		//await _yelpCollection.InsertOneAsync(new YelpItem(Guid.NewGuid(), userId, "toDelete", "toDelete", "toDelete", "toDelete"));

		//_yelpCollection = _database.GetCollection<YelpItem>("YelpItem");

		//var first = await _yelpCollection.FindAsync(x => x.UserId == userId);

		//_yelpCollection = await  p.Client.GetCollectionAsync<YelpItem>();

		return new List<YelpItem>();
	}
}
