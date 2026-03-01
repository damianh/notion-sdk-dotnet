// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Defines the sort order applied when searching Notion pages and databases.
/// </summary>
public sealed class SearchSort
{
    /// <summary>Gets the sort direction; either <c>"ascending"</c> or <c>"descending"</c>.</summary>
    [JsonPropertyName("direction")]
    public required string Direction { get; init; }  // "ascending" | "descending"

    /// <summary>Gets the timestamp field to sort by; defaults to <c>"last_edited_time"</c>.</summary>
    [JsonPropertyName("timestamp")]
    public string Timestamp { get; init; } = "last_edited_time";
}
