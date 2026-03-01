// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Properties.Values;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

internal sealed class PagesClient : IPagesClient
{
    private readonly NotionClient _client;

    internal PagesClient(NotionClient client) => _client = client;

    public Task<Page> Create(CreatePageRequest request, CancellationToken cancellationToken = default)
        => _client.Send<Page>(HttpMethod.Post, "pages", body: request, cancellationToken: cancellationToken);

    public Task<Page> Get(string pageId, CancellationToken cancellationToken = default)
        => _client.Send<Page>(HttpMethod.Get, $"pages/{pageId}", cancellationToken: cancellationToken);

    public Task<Page> Update(string pageId, UpdatePageRequest request, CancellationToken cancellationToken = default)
        => _client.Send<Page>(HttpMethod.Patch, $"pages/{pageId}", body: request, cancellationToken: cancellationToken);

    public Task<PropertyValue> GetProperty(
        string pageId,
        string propertyId,
        PaginationParameters? pagination = null,
        CancellationToken cancellationToken = default)
        => _client.Send<PropertyValue>(
            HttpMethod.Get,
            $"pages/{pageId}/properties/{propertyId}",
            query: pagination?.ToQueryParams(),
            cancellationToken: cancellationToken);
}
