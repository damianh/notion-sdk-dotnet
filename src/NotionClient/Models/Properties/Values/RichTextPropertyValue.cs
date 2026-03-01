// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>rich_text</c> property, containing formatted text content.
/// <see href="https://developers.notion.com/reference/property-value-object#rich-text-property-values"/>
/// </summary>
public sealed class RichTextPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "rich_text";

    /// <summary>The list of rich text segments that make up the formatted text content.</summary>
    [JsonPropertyName("rich_text")]
    public IReadOnlyList<RichTextItem> RichText { get; init; } = [];
}
