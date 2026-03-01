// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

internal sealed class DataSourcesClient : IDataSourcesClient
{
    private readonly NotionClient _client;

    internal DataSourcesClient(NotionClient client) => _client = client;

    public Task<DataSource> Get(string dataSourceId, CancellationToken cancellationToken = default)
        => _client.Send<DataSource>(HttpMethod.Get, $"databases/{dataSourceId}", cancellationToken: cancellationToken);

    public Task<DataSource> Create(CreateDatabaseRequest request, CancellationToken cancellationToken = default)
        => _client.Send<DataSource>(HttpMethod.Post, "databases", body: request, cancellationToken: cancellationToken);

    public Task<DataSource> Update(string dataSourceId, UpdateDatabaseRequest request, CancellationToken cancellationToken = default)
        => _client.Send<DataSource>(HttpMethod.Patch, $"databases/{dataSourceId}", body: request, cancellationToken: cancellationToken);

    public Task<PaginatedList<Page>> Query(
        string dataSourceId,
        QueryDatabaseRequest? request = null,
        CancellationToken cancellationToken = default)
        => _client.Send<PaginatedList<Page>>(
            HttpMethod.Post,
            $"databases/{dataSourceId}/query",
            body: request,
            cancellationToken: cancellationToken);
}
