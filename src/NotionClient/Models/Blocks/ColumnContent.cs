// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>The layout metadata of a <see cref="ColumnBlock"/>.</summary>
public sealed class ColumnContent
{
    /// <summary>Gets the optional proportional width of the column relative to its siblings in the column list.</summary>
    [JsonPropertyName("width_ratio")]
    public double? WidthRatio { get; init; }
}
