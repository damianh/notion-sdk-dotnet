// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>A shared content model carrying rich-text items, a block-level color, and optional nested child blocks.</summary>
public sealed class RichTextWithColorAndChildren
{
    /// <summary>Gets the rich-text items that form the block's text content.</summary>
    [JsonPropertyName("rich_text")]
    public IReadOnlyList<RichTextItem> RichText { get; init; } = [];

    /// <summary>Gets the text or background color applied to the block.</summary>
    [JsonPropertyName("color")]
    public ApiColor Color { get; init; } = ApiColor.Default;

    /// <summary>Gets the optional list of child blocks nested beneath this block.</summary>
    [JsonPropertyName("children")]
    public IReadOnlyList<Block>? Children { get; init; }
}
