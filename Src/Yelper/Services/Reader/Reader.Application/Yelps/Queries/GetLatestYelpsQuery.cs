﻿using MediatR;
using Reader.Application.Yelps.Common;

namespace Reader.Application.Yelps.Queries;

public record GetLatestYelpsQuery : IRequest<List<YelpItem>>;
