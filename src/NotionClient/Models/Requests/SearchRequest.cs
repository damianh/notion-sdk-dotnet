// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for the <see href="https://developers.notion.com/reference/post-search">Search</see> API endpoint.
/// Searches all pages and databases that the integration has access to.
/// </summary>
public sealed class SearchRequest
{
    /// <summary>Gets the text to search for across page and database titles, or <see langword="null"/> to return all accessible objects.</summary>
    [JsonPropertyName("query")]
    public string? Query { get; init; }

    /// <summary>Gets an optional filter to restrict results to either pages or databases only.</summary>
    [JsonPropertyName("filter")]
    public SearchFilter? Filter { get; init; }

    /// <summary>Gets an optional sort to order results by <c>last_edited_time</c> ascending or descending.</summary>
    [JsonPropertyName("sort")]
    public SearchSort? Sort { get; init; }

    /// <summary>Gets the pagination cursor from a previous response to retrieve the next page of results.</summary>
    [JsonPropertyName("start_cursor")]
    public string? StartCursor { get; init; }

    /// <summary>Gets the maximum number of results to return per page, between 1 and 100. Defaults to 100 if not specified.</summary>
    [JsonPropertyName("page_size")]
    public int? PageSize { get; init; }
}
