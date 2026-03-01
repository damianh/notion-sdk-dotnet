// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>created_time</c> property, containing the ISO 8601 timestamp
/// of when this page was created.
/// <see href="https://developers.notion.com/reference/property-value-object#created-time-property-values"/>
/// </summary>
public sealed class CreatedTimePropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "created_time";

    /// <summary>The ISO 8601 date-time string recording when this page was created.</summary>
    [JsonPropertyName("created_time")]
    public string CreatedTime { get; init; } = null!;
}
