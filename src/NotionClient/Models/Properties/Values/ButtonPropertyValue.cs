// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>button</c> property.
/// Button properties have no stored value; the property object is included for type identification only.
/// <see href="https://developers.notion.com/reference/property-value-object#button"/>
/// </summary>
public sealed class ButtonPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "button";

    /// <summary>The button value object (currently empty in the Notion API).</summary>
    [JsonPropertyName("button")]
    public object? Button { get; init; }
}
