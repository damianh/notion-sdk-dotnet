// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>date</c> property, which stores a date or date range value.
/// <see href="https://developers.notion.com/reference/property-object#date"/>
/// </summary>
public sealed class DatePropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "date";

    /// <summary>The date configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("date")]
    public object? Date { get; init; }
}
