// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>checkbox</c> property, which stores a boolean true/false value.
/// <see href="https://developers.notion.com/reference/property-object#checkbox"/>
/// </summary>
public sealed class CheckboxPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "checkbox";

    /// <summary>The checkbox configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("checkbox")]
    public object? Checkbox { get; init; }
}
