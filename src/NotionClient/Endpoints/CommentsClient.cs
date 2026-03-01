// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

internal sealed class CommentsClient : ICommentsClient
{
    private readonly NotionClient _client;

    internal CommentsClient(NotionClient client) => _client = client;

    public Task<Comment> Create(CreateCommentRequest request, CancellationToken cancellationToken = default)
        => _client.Send<Comment>(HttpMethod.Post, "comments", body: request, cancellationToken: cancellationToken);

    public Task<PaginatedList<Comment>> List(
        string blockId,
        PaginationParameters? pagination = null,
        CancellationToken cancellationToken = default)
    {
        var query = pagination?.ToQueryParams() ?? new Dictionary<string, string?>();
        query["block_id"] = blockId;
        return _client.Send<PaginatedList<Comment>>(
            HttpMethod.Get,
            "comments",
            query: query,
            cancellationToken: cancellationToken);
    }
}
