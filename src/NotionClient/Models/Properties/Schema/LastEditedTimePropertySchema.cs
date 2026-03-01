// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>last_edited_time</c> property, which automatically records the timestamp
/// of the most recent edit to the page.
/// <see href="https://developers.notion.com/reference/property-object#last-edited-time"/>
/// </summary>
public sealed class LastEditedTimePropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "last_edited_time";

    /// <summary>The last_edited_time configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("last_edited_time")]
    public object? LastEditedTime { get; init; }
}
