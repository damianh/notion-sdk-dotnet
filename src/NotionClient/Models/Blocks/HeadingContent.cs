// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>The content payload shared by <see cref="Heading1Block"/>, <see cref="Heading2Block"/>, and <see cref="Heading3Block"/>.</summary>
public sealed class HeadingContent
{
    /// <summary>Gets the rich-text items that form the heading text.</summary>
    [JsonPropertyName("rich_text")]
    public IReadOnlyList<RichTextItem> RichText { get; init; } = [];

    /// <summary>Gets the text or background color applied to the heading.</summary>
    [JsonPropertyName("color")]
    public ApiColor Color { get; init; } = ApiColor.Default;

    /// <summary>Gets a value indicating whether the heading can be toggled to show or hide its child blocks.</summary>
    [JsonPropertyName("is_toggleable")]
    public bool IsToggleable { get; init; }
}
