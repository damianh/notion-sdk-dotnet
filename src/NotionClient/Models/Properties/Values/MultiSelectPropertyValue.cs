// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>multi_select</c> property, containing the list of selected options.
/// <see href="https://developers.notion.com/reference/property-value-object#multi-select-property-values"/>
/// </summary>
public sealed class MultiSelectPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "multi_select";

    /// <summary>The list of currently selected options for this property.</summary>
    [JsonPropertyName("multi_select")]
    public IReadOnlyList<SelectOption> MultiSelect { get; init; } = [];
}
