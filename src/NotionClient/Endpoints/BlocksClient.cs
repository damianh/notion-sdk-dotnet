// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models.Blocks;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

internal sealed class BlocksClient : IBlocksClient
{
    private readonly NotionClient _client;

    internal BlocksClient(NotionClient client) => _client = client;

    public Task<Block> Get(string blockId, CancellationToken cancellationToken = default)
        => _client.Send<Block>(HttpMethod.Get, $"blocks/{blockId}", cancellationToken: cancellationToken);

    public Task<Block> Update(string blockId, Block block, CancellationToken cancellationToken = default)
        => _client.Send<Block>(HttpMethod.Patch, $"blocks/{blockId}", body: block, cancellationToken: cancellationToken);

    public Task<Block> Delete(string blockId, CancellationToken cancellationToken = default)
        => _client.Send<Block>(HttpMethod.Delete, $"blocks/{blockId}", cancellationToken: cancellationToken);

    public Task<PaginatedList<Block>> ListChildren(
        string blockId,
        PaginationParameters? pagination = null,
        CancellationToken cancellationToken = default)
        => _client.Send<PaginatedList<Block>>(
            HttpMethod.Get,
            $"blocks/{blockId}/children",
            query: pagination?.ToQueryParams(),
            cancellationToken: cancellationToken);

    public Task<PaginatedList<Block>> AppendChildren(
        string blockId,
        AppendBlockChildrenRequest request,
        CancellationToken cancellationToken = default)
        => _client.Send<PaginatedList<Block>>(
            HttpMethod.Patch,
            $"blocks/{blockId}/children",
            body: request,
            cancellationToken: cancellationToken);
}
