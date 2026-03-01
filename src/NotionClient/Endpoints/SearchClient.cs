// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

internal sealed class SearchClient : ISearchClient
{
    private readonly NotionClient _client;

    internal SearchClient(NotionClient client) => _client = client;

    public Task<PaginatedList<SearchResult>> Search(
        SearchRequest? request = null,
        CancellationToken cancellationToken = default)
        => _client.Send<PaginatedList<SearchResult>>(
            HttpMethod.Post,
            "search",
            body: request,
            cancellationToken: cancellationToken);
}
