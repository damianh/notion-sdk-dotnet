// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>title</c> property, containing the page title as rich text.
/// <see href="https://developers.notion.com/reference/property-value-object#title-property-values"/>
/// </summary>
public sealed class TitlePropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "title";

    /// <summary>The rich text segments that compose the page title.</summary>
    [JsonPropertyName("title")]
    public IReadOnlyList<RichTextItem> Title { get; init; } = [];
}
