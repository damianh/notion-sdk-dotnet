// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

/// <summary>
/// Operations on Notion comments — discussion threads attached to pages or specific blocks.
/// <see href="https://developers.notion.com/reference/comment-object"/>
/// </summary>
public interface ICommentsClient
{
    /// <summary>Creates a new comment on a page or inside an existing discussion thread.</summary>
    Task<Comment> Create(CreateCommentRequest request, CancellationToken cancellationToken = default);

    /// <summary>Returns a paginated list of unresolved comments on the specified block or page.</summary>
    Task<PaginatedList<Comment>> List(string blockId, PaginationParameters? pagination = null, CancellationToken cancellationToken = default);
}
