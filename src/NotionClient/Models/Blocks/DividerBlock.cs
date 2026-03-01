// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion divider block that renders a horizontal rule to visually separate sections of content.
/// </summary>
public sealed class DividerBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "divider";

    /// <summary>Gets the divider data object. This is always an empty object in the Notion API.</summary>
    [JsonPropertyName("divider")]
    public object? Divider { get; init; }
}
