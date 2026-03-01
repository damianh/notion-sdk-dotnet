// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>Filter on <c>created_time</c> or <c>last_edited_time</c> metadata.</summary>
public sealed class TimestampFilter : Filter
{
    /// <summary>Gets the timestamp field to filter by; either <c>"created_time"</c> or <c>"last_edited_time"</c>.</summary>
    [JsonPropertyName("timestamp")]
    public required string Timestamp { get; init; }  // "created_time" | "last_edited_time"

    /// <summary>Gets the date filter condition applied to the <c>created_time</c> metadata field.</summary>
    [JsonPropertyName("created_time")]
    public DateFilterCondition? CreatedTime { get; init; }

    /// <summary>Gets the date filter condition applied to the <c>last_edited_time</c> metadata field.</summary>
    [JsonPropertyName("last_edited_time")]
    public DateFilterCondition? LastEditedTime { get; init; }
}
