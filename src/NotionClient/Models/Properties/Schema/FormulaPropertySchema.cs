// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>formula</c> property, which computes a value from an expression
/// using other properties in the same database.
/// <see href="https://developers.notion.com/reference/property-object#formula"/>
/// </summary>
public sealed class FormulaPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "formula";

    /// <summary>The formula expression configuration for this property.</summary>
    [JsonPropertyName("formula")]
    public FormulaConfig? Formula { get; init; }
}
