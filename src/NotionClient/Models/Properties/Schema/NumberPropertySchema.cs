// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>number</c> property, which stores a numeric value with optional formatting.
/// <see href="https://developers.notion.com/reference/property-object#number"/>
/// </summary>
public sealed class NumberPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "number";

    /// <summary>The number formatting configuration for this property.</summary>
    [JsonPropertyName("number")]
    public NumberConfig? Number { get; init; }
}
