// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Abstract base class for the aggregated result of a Notion rollup property.
/// Subtypes correspond to the result type: <see cref="NumberRollupResult"/> (number),
/// <see cref="DateRollupResult"/> (date), <see cref="ArrayRollupResult"/> (array),
/// <see cref="UnsupportedRollupResult"/> (unsupported).
/// <see href="https://developers.notion.com/reference/property-value-object#rollup-property-values"/>
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(NumberRollupResult), "number")]
[JsonDerivedType(typeof(DateRollupResult), "date")]
[JsonDerivedType(typeof(ArrayRollupResult), "array")]
[JsonDerivedType(typeof(UnsupportedRollupResult), "unsupported")]
public abstract class RollupResult
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public RollupResult() { }

    /// <summary>The rollup result type discriminator: "number", "date", "array", or "unsupported".</summary>
    [JsonIgnore]
    public virtual string RollupType => string.Empty;

    /// <summary>The rollup function used (e.g., "count", "sum", etc.).</summary>
    [JsonPropertyName("function")]
    public string? Function { get; init; }
}
