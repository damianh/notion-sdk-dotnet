// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion quote block that renders rich text as a styled block quotation, visually indented to stand out from body text.
/// </summary>
public sealed class QuoteBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "quote";

    /// <summary>Gets the quote content including rich text, color, and optional nested child blocks.</summary>
    [JsonPropertyName("quote")]
    public RichTextWithColorAndChildren Quote { get; init; } = null!;
}
