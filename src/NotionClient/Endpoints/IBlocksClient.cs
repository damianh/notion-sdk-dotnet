// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models.Blocks;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

/// <summary>
/// Operations on Notion blocks — the content elements that make up a page
/// (paragraphs, headings, lists, embeds, etc.).
/// <see href="https://developers.notion.com/reference/block"/>
/// </summary>
public interface IBlocksClient
{
    /// <summary>Retrieves a block by its identifier.</summary>
    Task<Block> Get(string blockId, CancellationToken cancellationToken = default);

    /// <summary>Updates the content or appearance of an existing block.</summary>
    Task<Block> Update(string blockId, Block block, CancellationToken cancellationToken = default);

    /// <summary>Archives (soft-deletes) a block and its children.</summary>
    Task<Block> Delete(string blockId, CancellationToken cancellationToken = default);

    /// <summary>Returns a paginated list of the child blocks nested under the specified block or page.</summary>
    Task<PaginatedList<Block>> ListChildren(string blockId, PaginationParameters? pagination = null, CancellationToken cancellationToken = default);

    /// <summary>Appends new child blocks to the specified parent block or page.</summary>
    Task<PaginatedList<Block>> AppendChildren(string blockId, AppendBlockChildrenRequest request, CancellationToken cancellationToken = default);
}
