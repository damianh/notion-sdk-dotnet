// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion heading 2 block — the second-largest heading level (H2) on a Notion page.
/// </summary>
public sealed class Heading2Block : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "heading_2";

    /// <summary>Gets the heading content including rich text, color, and whether the heading can be toggled to show/hide child blocks.</summary>
    [JsonPropertyName("heading_2")]
    public HeadingContent Heading2 { get; init; } = null!;
}
