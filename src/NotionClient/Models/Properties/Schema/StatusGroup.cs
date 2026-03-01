// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// A named group of status options in a Notion <c>status</c> property schema.
/// Groups categorize status options into stages such as "To-do", "In progress", or "Complete".
/// </summary>
public sealed class StatusGroup
{
    /// <summary>The unique identifier of this status group.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The display name of this group (e.g., "To-do", "In progress", "Complete").</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    /// <summary>The color associated with this status group.</summary>
    [JsonPropertyName("color")]
    public SelectColor Color { get; init; } = SelectColor.Default;

    /// <summary>The IDs of the status options that belong to this group.</summary>
    [JsonPropertyName("option_ids")]
    public IReadOnlyList<string> OptionIds { get; init; } = [];
}
