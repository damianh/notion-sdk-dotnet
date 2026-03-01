// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>A sort applied to a database query.</summary>
public sealed class Sort
{
    /// <summary>Gets the name of the database property to sort by. Mutually exclusive with <see cref="Timestamp"/>.</summary>
    [JsonPropertyName("property")]
    public string? Property { get; init; }

    /// <summary>For timestamp sorts: "created_time" or "last_edited_time".</summary>
    [JsonPropertyName("timestamp")]
    public string? Timestamp { get; init; }

    /// <summary>"ascending" or "descending".</summary>
    [JsonPropertyName("direction")]
    public required string Direction { get; init; }
}
