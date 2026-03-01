// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>select</c> property, containing the single selected option.
/// <see href="https://developers.notion.com/reference/property-value-object#select-property-values"/>
/// </summary>
public sealed class SelectPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "select";

    /// <summary>The currently selected option, or <c>null</c> if no option is selected.</summary>
    [JsonPropertyName("select")]
    public SelectOption? Select { get; init; }
}
