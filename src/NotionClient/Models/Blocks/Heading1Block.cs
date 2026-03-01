// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion heading 1 block — the largest heading level (H1) on a Notion page.
/// </summary>
public sealed class Heading1Block : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "heading_1";

    /// <summary>Gets the heading content including rich text, color, and whether the heading can be toggled to show/hide child blocks.</summary>
    [JsonPropertyName("heading_1")]
    public HeadingContent Heading1 { get; init; } = null!;
}
