// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

/// <summary>
/// Full-text search across all pages and databases the integration can access.
/// <see href="https://developers.notion.com/reference/post-search"/>
/// </summary>
public interface ISearchClient
{
    /// <summary>Searches pages and databases by title, optionally filtered by object type and sorted by relevance or last-edited time.</summary>
    Task<PaginatedList<SearchResult>> Search(SearchRequest? request = null, CancellationToken cancellationToken = default);
}
