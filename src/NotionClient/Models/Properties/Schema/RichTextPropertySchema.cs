// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>rich_text</c> property, which stores formatted text content.
/// <see href="https://developers.notion.com/reference/property-object#rich-text"/>
/// </summary>
public sealed class RichTextPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "rich_text";

    /// <summary>The rich_text configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("rich_text")]
    public object? RichText { get; init; }
}
