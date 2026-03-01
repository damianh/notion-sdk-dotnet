// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Represents a single option in a Notion <c>status</c> property (e.g., "Not started", "In progress").
/// </summary>
public sealed class StatusOption
{
    /// <summary>The unique identifier of this status option.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The display name of this status option.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    /// <summary>The color used to render this status option in the Notion UI.</summary>
    [JsonPropertyName("color")]
    public SelectColor Color { get; init; } = SelectColor.Default;
}
