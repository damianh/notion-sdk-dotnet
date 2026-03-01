// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>created_time</c> property, which automatically records the timestamp when a page was created.
/// <see href="https://developers.notion.com/reference/property-object#created-time"/>
/// </summary>
public sealed class CreatedTimePropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "created_time";

    /// <summary>The created_time configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("created_time")]
    public object? CreatedTime { get; init; }
}
