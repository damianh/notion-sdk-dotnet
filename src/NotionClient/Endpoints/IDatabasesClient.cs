// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

/// <summary>
/// Operations on Notion databases — collections of pages with a typed property schema
/// that can be queried, filtered, and sorted.
/// <see href="https://developers.notion.com/reference/database"/>
/// </summary>
public interface IDatabasesClient
{
    /// <summary>Retrieves a database by its identifier, including its property schema.</summary>
    Task<Database> Get(string databaseId, CancellationToken cancellationToken = default);

    /// <summary>Creates a new database as a child of the specified parent page.</summary>
    Task<Database> Create(CreateDatabaseRequest request, CancellationToken cancellationToken = default);

    /// <summary>Updates a database's title, description, or property schema.</summary>
    Task<Database> Update(string databaseId, UpdateDatabaseRequest request, CancellationToken cancellationToken = default);

    /// <summary>Queries a database and returns a paginated list of pages that match the filter and sort criteria.</summary>
    Task<PaginatedList<Page>> Query(string databaseId, QueryDatabaseRequest? request = null, CancellationToken cancellationToken = default);
}
