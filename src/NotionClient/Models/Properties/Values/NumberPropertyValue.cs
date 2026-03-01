// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>number</c> property, containing a numeric value.
/// <see href="https://developers.notion.com/reference/property-value-object#number-property-values"/>
/// </summary>
public sealed class NumberPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "number";

    /// <summary>The numeric value stored in this property, or <c>null</c> if not set.</summary>
    [JsonPropertyName("number")]
    public double? Number { get; init; }
}
