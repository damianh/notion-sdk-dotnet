// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>A shared content model that carries a URL and an optional rich-text caption, used by blocks such as <see cref="EmbedBlock"/>.</summary>
public sealed class UrlContent
{
    /// <summary>Gets the URL of the external resource (e.g. the embedded page URL).</summary>
    [JsonPropertyName("url")]
    public string Url { get; init; } = null!;

    /// <summary>Gets the optional rich-text caption displayed beneath the content block.</summary>
    [JsonPropertyName("caption")]
    public IReadOnlyList<RichTextItem> Caption { get; init; } = [];
}
