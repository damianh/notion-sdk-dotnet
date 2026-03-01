// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// Text formatting annotations applied to a rich text segment.
/// </summary>
public sealed class RichTextAnnotations
{
    /// <summary>Gets a value indicating whether the text is rendered in bold.</summary>
    [JsonPropertyName("bold")]
    public bool Bold { get; init; }

    /// <summary>Gets a value indicating whether the text is rendered in italics.</summary>
    [JsonPropertyName("italic")]
    public bool Italic { get; init; }

    /// <summary>Gets a value indicating whether the text has a horizontal line through the middle.</summary>
    [JsonPropertyName("strikethrough")]
    public bool Strikethrough { get; init; }

    /// <summary>Gets a value indicating whether the text has an underline.</summary>
    [JsonPropertyName("underline")]
    public bool Underline { get; init; }

    /// <summary>Gets a value indicating whether the text is rendered as inline code with a monospace font.</summary>
    [JsonPropertyName("code")]
    public bool Code { get; init; }

    /// <summary>Gets the foreground or background color applied to the text segment.</summary>
    [JsonPropertyName("color")]
    public ApiColor Color { get; init; } = ApiColor.Default;
}
