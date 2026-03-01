// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Pagination;

/// <summary>
/// A paginated list response from the Notion API.
/// Use <c>HasMore</c> and <c>NextCursor</c> to fetch subsequent pages.
/// </summary>
/// <typeparam name="T">The item type in the <c>results</c> array.</typeparam>
public sealed class PaginatedList<T>
{
    /// <summary>Always "list" — identifies this response as a paginated list object.</summary>
    [JsonPropertyName("object")]
    public string Object { get; init; } = "list";

    /// <summary>The items returned in this page of results.</summary>
    [JsonPropertyName("results")]
    public IReadOnlyList<T> Results { get; init; } = [];

    /// <summary>Cursor to pass as <c>start_cursor</c> on the next request, or <c>null</c> when on the last page.</summary>
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; init; }

    /// <summary><c>true</c> when there are additional pages to fetch.</summary>
    [JsonPropertyName("has_more")]
    public bool HasMore { get; init; }

    /// <summary>The type string of the items in this list (e.g., "block", "page", "user").</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}
