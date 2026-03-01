// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>multi_select</c> property, which allows selecting zero or more options
/// from a predefined set.
/// <see href="https://developers.notion.com/reference/property-object#multi-select"/>
/// </summary>
public sealed class MultiSelectPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "multi_select";

    /// <summary>The multi-select configuration including available options.</summary>
    [JsonPropertyName("multi_select")]
    public MultiSelectConfig? MultiSelect { get; init; }
}
