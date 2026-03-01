// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>The content payload of a <see cref="BookmarkBlock"/>.</summary>
public sealed class BookmarkContent
{
    /// <summary>Gets the URL of the bookmarked web page.</summary>
    [JsonPropertyName("url")]
    public string Url { get; init; } = null!;

    /// <summary>Gets the optional rich-text caption displayed beneath the bookmark card.</summary>
    [JsonPropertyName("caption")]
    public IReadOnlyList<RichTextItem> Caption { get; init; } = [];
}
