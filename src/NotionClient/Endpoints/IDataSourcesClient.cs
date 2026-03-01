// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

/// <summary>
/// Client for the Notion data-sources endpoint, which provides access to external data sources
/// connected to a Notion workspace.
/// </summary>
/// <remarks>
/// See <see href="https://developers.notion.com/reference/retrieve-a-datasource">Notion API — Data sources</see>.
/// </remarks>
public interface IDataSourcesClient
{
    /// <summary>
    /// Retrieves a data source by its identifier.
    /// </summary>
    /// <param name="dataSourceId">The identifier of the data source to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The requested <see cref="DataSource"/>.</returns>
    Task<DataSource> Get(string dataSourceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new data source backed by a Notion database.
    /// </summary>
    /// <param name="request">The database creation parameters including title, properties, and parent.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The newly created <see cref="DataSource"/>.</returns>
    Task<DataSource> Create(CreateDatabaseRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing data source (e.g., its title or property schema).
    /// </summary>
    /// <param name="dataSourceId">The identifier of the data source to update.</param>
    /// <param name="request">The update parameters.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>The updated <see cref="DataSource"/>.</returns>
    Task<DataSource> Update(string dataSourceId, UpdateDatabaseRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Queries a data source for its pages, optionally applying filters and sorts.
    /// </summary>
    /// <param name="dataSourceId">The identifier of the data source to query.</param>
    /// <param name="request">Optional filter, sort, and pagination parameters. Pass <see langword="null"/> to retrieve all pages.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A paginated list of <see cref="Page"/> results.</returns>
    Task<PaginatedList<Page>> Query(string dataSourceId, QueryDatabaseRequest? request = null, CancellationToken cancellationToken = default);
}
