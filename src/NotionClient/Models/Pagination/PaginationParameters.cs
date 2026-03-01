// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Pagination;

/// <summary>
/// Common pagination query parameters for list/query endpoints.
/// </summary>
public sealed class PaginationParameters
{
    /// <summary>Cursor from the previous page's <c>next_cursor</c> field.</summary>
    [JsonPropertyName("start_cursor")]
    public string? StartCursor { get; init; }

    /// <summary>Maximum number of items to return (1–100, default 100).</summary>
    [JsonPropertyName("page_size")]
    public int? PageSize { get; init; }

    /// <summary>Convert to query-string dictionary for GET requests.</summary>
    public IDictionary<string, string?> ToQueryParams()
    {
        var dict = new Dictionary<string, string?>();
        if (StartCursor is not null)
        {
            dict["start_cursor"] = StartCursor;
        }

        if (PageSize is not null)
        {
            dict["page_size"] = PageSize.Value.ToString();
        }
        return dict;
    }
}
