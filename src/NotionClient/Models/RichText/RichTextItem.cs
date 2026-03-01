// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A rich text item — the union of text, mention, and equation segments.
/// Discriminated via the "type" field.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(TextRichTextItem), "text")]
[JsonDerivedType(typeof(MentionRichTextItem), "mention")]
[JsonDerivedType(typeof(EquationRichTextItem), "equation")]
public abstract class RichTextItem
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public RichTextItem() { }

    /// <summary>The rich text type discriminator string: "text", "mention", or "equation".</summary>
    [JsonIgnore]
    public virtual string Type => string.Empty;

    /// <summary>The plain text content, without any styling.</summary>
    [JsonPropertyName("plain_text")]
    public string PlainText { get; init; } = string.Empty;

    /// <summary>URL that this rich text links to or mentions, if any.</summary>
    [JsonPropertyName("href")]
    public string? Href { get; init; }

    /// <summary>Formatting annotations applied to this segment.</summary>
    [JsonPropertyName("annotations")]
    public RichTextAnnotations? Annotations { get; init; }
}
