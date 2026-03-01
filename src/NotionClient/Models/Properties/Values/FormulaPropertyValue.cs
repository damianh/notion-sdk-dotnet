// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>formula</c> property, containing the evaluated result
/// of the formula expression.
/// <see href="https://developers.notion.com/reference/property-value-object#formula-property-values"/>
/// </summary>
public sealed class FormulaPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "formula";

    /// <summary>The computed formula result, which may be a string, number, boolean, or date.</summary>
    [JsonPropertyName("formula")]
    public FormulaResult Formula { get; init; } = null!;
}
