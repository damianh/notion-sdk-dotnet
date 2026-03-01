// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>The content payload of a <see cref="CalloutBlock"/>.</summary>
public sealed class CalloutContent
{
    /// <summary>Gets the rich-text items that form the callout's body text.</summary>
    [JsonPropertyName("rich_text")]
    public IReadOnlyList<RichTextItem> RichText { get; init; } = [];

    /// <summary>Gets the background or text color applied to the callout block.</summary>
    [JsonPropertyName("color")]
    public ApiColor Color { get; init; } = ApiColor.Default;

    /// <summary>Gets the optional emoji or external image icon displayed alongside the callout text.</summary>
    [JsonPropertyName("icon")]
    public Icon? Icon { get; init; }
}
