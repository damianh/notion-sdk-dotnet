// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A rich text item containing an inline mention (e.g., a linked user, page, or database).
/// </summary>
public sealed class MentionRichTextItem : RichTextItem
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "mention";

    /// <summary>The mention details, identifying the referenced object type and its data.</summary>
    [JsonPropertyName("mention")]
    public required Mention Mention { get; init; }
}
