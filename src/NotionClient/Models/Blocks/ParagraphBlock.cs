// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion paragraph block containing rich-text body content with optional nested child blocks.
/// </summary>
public sealed class ParagraphBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "paragraph";

    /// <summary>Gets the paragraph content including rich text, color, and optional child blocks.</summary>
    [JsonPropertyName("paragraph")]
    public RichTextWithColorAndChildren Paragraph { get; init; } = null!;
}
