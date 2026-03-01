// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>date</c> property, containing a start date and optional end date.
/// <see href="https://developers.notion.com/reference/property-value-object#date-property-values"/>
/// </summary>
public sealed class DatePropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "date";

    /// <summary>The date or date range value stored in this property, or <c>null</c> if not set.</summary>
    [JsonPropertyName("date")]
    public DateValue? Date { get; init; }
}
