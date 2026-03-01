// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A rich text item containing plain or formatted text content.
/// </summary>
public sealed class TextRichTextItem : RichTextItem
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "text";

    /// <summary>The text content and optional inline link for this segment.</summary>
    [JsonPropertyName("text")]
    public required TextContent Text { get; init; }
}
