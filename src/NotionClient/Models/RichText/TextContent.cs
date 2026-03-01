// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// The textual content and optional hyperlink for a <see cref="TextRichTextItem"/>.
/// </summary>
public sealed class TextContent
{
    /// <summary>The raw text string for this segment.</summary>
    [JsonPropertyName("content")]
    public required string Content { get; init; }

    /// <summary>An optional hyperlink attached to this text segment, or <c>null</c> if there is no link.</summary>
    [JsonPropertyName("link")]
    public TextLink? Link { get; init; }
}
