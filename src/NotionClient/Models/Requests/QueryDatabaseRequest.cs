// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Filters;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for the <see href="https://developers.notion.com/reference/post-database-query">Query a database</see> API endpoint.
/// Retrieves pages (rows) from a database, with optional filtering, sorting, and pagination.
/// </summary>
public sealed class QueryDatabaseRequest
{
    /// <summary>Gets the filter criteria to apply when querying the database, or <see langword="null"/> to return all rows.</summary>
    [JsonPropertyName("filter")]
    public Filter? Filter { get; init; }

    /// <summary>Gets the list of sort criteria to order the database rows. Multiple sorts are applied in order.</summary>
    [JsonPropertyName("sorts")]
    public IReadOnlyList<Sort>? Sorts { get; init; }

    /// <summary>Gets the pagination cursor from a previous response to retrieve the next page of results.</summary>
    [JsonPropertyName("start_cursor")]
    public string? StartCursor { get; init; }

    /// <summary>Gets the maximum number of results to return per page, between 1 and 100. Defaults to 100 if not specified.</summary>
    [JsonPropertyName("page_size")]
    public int? PageSize { get; init; }

    /// <summary>Gets the list of property IDs to include in the response, allowing partial property retrieval for large databases.</summary>
    [JsonPropertyName("filter_properties")]
    public IReadOnlyList<string>? FilterProperties { get; init; }
}
