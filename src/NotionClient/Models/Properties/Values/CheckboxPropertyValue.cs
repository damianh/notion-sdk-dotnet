// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>checkbox</c> property, representing a boolean checked/unchecked state.
/// <see href="https://developers.notion.com/reference/property-value-object#checkbox-property-values"/>
/// </summary>
public sealed class CheckboxPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "checkbox";

    /// <summary><c>true</c> when the checkbox is checked; <c>false</c> when unchecked.</summary>
    [JsonPropertyName("checkbox")]
    public bool Checkbox { get; init; }
}
