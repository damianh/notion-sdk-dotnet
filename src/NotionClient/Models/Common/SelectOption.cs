// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// A select or multi-select option, as returned in database property schemas and page property values.
/// </summary>
public sealed class SelectOption
{
    /// <summary>Gets the unique identifier of the option, assigned by Notion. <see langword="null"/> when creating a new option.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Gets the display name of the option as it appears in the select or multi-select property.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Gets the badge color used to visually distinguish this option in the Notion UI.</summary>
    [JsonPropertyName("color")]
    public SelectColor Color { get; init; } = SelectColor.Default;

    /// <summary>Gets an optional description for the option, providing additional context. Only available on status property options.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }
}
