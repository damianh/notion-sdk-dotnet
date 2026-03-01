// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Abstract base class for the computed result of a Notion formula property.
/// Subtypes correspond to the result type: <see cref="StringFormulaResult"/> (string),
/// <see cref="NumberFormulaResult"/> (number), <see cref="BooleanFormulaResult"/> (boolean),
/// <see cref="DateFormulaResult"/> (date).
/// <see href="https://developers.notion.com/reference/property-value-object#formula-property-values"/>
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(StringFormulaResult), "string")]
[JsonDerivedType(typeof(NumberFormulaResult), "number")]
[JsonDerivedType(typeof(BooleanFormulaResult), "boolean")]
[JsonDerivedType(typeof(DateFormulaResult), "date")]
public abstract class FormulaResult
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public FormulaResult() { }

    /// <summary>The formula result type discriminator: "string", "number", "boolean", or "date".</summary>
    [JsonIgnore]
    public virtual string FormulaType => string.Empty;
}
