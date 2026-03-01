// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion heading 3 block — the smallest heading level (H3) on a Notion page.
/// </summary>
public sealed class Heading3Block : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "heading_3";

    /// <summary>Gets the heading content including rich text, color, and whether the heading can be toggled to show/hide child blocks.</summary>
    [JsonPropertyName("heading_3")]
    public HeadingContent Heading3 { get; init; } = null!;
}
