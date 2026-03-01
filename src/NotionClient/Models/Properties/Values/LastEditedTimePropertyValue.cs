// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>last_edited_time</c> property, containing the ISO 8601 timestamp
/// of the most recent edit to this page.
/// <see href="https://developers.notion.com/reference/property-value-object#last-edited-time-property-values"/>
/// </summary>
public sealed class LastEditedTimePropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "last_edited_time";

    /// <summary>The ISO 8601 date-time string of the most recent edit to this page.</summary>
    [JsonPropertyName("last_edited_time")]
    public string LastEditedTime { get; init; } = null!;
}
