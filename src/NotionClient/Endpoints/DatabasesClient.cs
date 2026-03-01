// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

internal sealed class DatabasesClient : IDatabasesClient
{
    private readonly NotionClient _client;

    internal DatabasesClient(NotionClient client) => _client = client;

    public Task<Database> Get(string databaseId, CancellationToken cancellationToken = default)
        => _client.Send<Database>(HttpMethod.Get, $"databases/{databaseId}", cancellationToken: cancellationToken);

    public Task<Database> Create(CreateDatabaseRequest request, CancellationToken cancellationToken = default)
        => _client.Send<Database>(HttpMethod.Post, "databases", body: request, cancellationToken: cancellationToken);

    public Task<Database> Update(string databaseId, UpdateDatabaseRequest request, CancellationToken cancellationToken = default)
        => _client.Send<Database>(HttpMethod.Patch, $"databases/{databaseId}", body: request, cancellationToken: cancellationToken);

    public Task<PaginatedList<Page>> Query(
        string databaseId,
        QueryDatabaseRequest? request = null,
        CancellationToken cancellationToken = default)
        => _client.Send<PaginatedList<Page>>(
            HttpMethod.Post,
            $"databases/{databaseId}/query",
            body: request,
            cancellationToken: cancellationToken);
}
