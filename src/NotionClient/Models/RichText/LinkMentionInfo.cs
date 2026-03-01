// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// Metadata for a <c>link_mention</c> rich text mention, including the URL and optional
/// preview data such as title, description, and media assets.
/// </summary>
public sealed class LinkMentionInfo
{
    /// <summary>The URL of the linked resource.</summary>
    [JsonPropertyName("href")]
    public required string Href { get; init; }

    /// <summary>The title of the linked resource, or <c>null</c> if not available.</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>A short description of the linked resource, or <c>null</c> if not available.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>The author of the linked content, or <c>null</c> if not available.</summary>
    [JsonPropertyName("link_author")]
    public string? LinkAuthor { get; init; }

    /// <summary>The name of the link provider or site, or <c>null</c> if not available.</summary>
    [JsonPropertyName("link_provider")]
    public string? LinkProvider { get; init; }

    /// <summary>URL of the thumbnail image for this link, or <c>null</c> if not available.</summary>
    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; init; }

    /// <summary>URL of the icon image for the link provider, or <c>null</c> if not available.</summary>
    [JsonPropertyName("icon_url")]
    public string? IconUrl { get; init; }

    /// <summary>URL for an embeddable iframe of this link, or <c>null</c> if not embeddable.</summary>
    [JsonPropertyName("iframe_url")]
    public string? IframeUrl { get; init; }

    /// <summary>The height of the embedded iframe in pixels, or <c>null</c> if not applicable.</summary>
    [JsonPropertyName("height")]
    public int? Height { get; init; }

    /// <summary>The padding around the embedded iframe in pixels, or <c>null</c> if not applicable.</summary>
    [JsonPropertyName("padding")]
    public int? Padding { get; init; }

    /// <summary>The top padding of the embedded iframe in pixels, or <c>null</c> if not applicable.</summary>
    [JsonPropertyName("padding_top")]
    public int? PaddingTop { get; init; }
}
