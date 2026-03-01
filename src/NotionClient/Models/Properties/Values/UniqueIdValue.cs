// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// The unique identifier value stored in a Notion <c>unique_id</c> property,
/// composed of an auto-incremented number and an optional prefix string.
/// </summary>
public sealed class UniqueIdValue
{
    /// <summary>The auto-incremented sequential number portion of the unique ID.</summary>
    [JsonPropertyName("number")]
    public int? Number { get; init; }

    /// <summary>The optional prefix prepended to the number (e.g., "TASK" in "TASK-42"), or <c>null</c> if none.</summary>
    [JsonPropertyName("prefix")]
    public string? Prefix { get; init; }
}
