// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// A rollup result containing an array of property values from the related pages.
/// </summary>
public sealed class ArrayRollupResult : RollupResult
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string RollupType => "array";

    /// <summary>The collection of individual property values aggregated from the related pages.</summary>
    [JsonPropertyName("array")]
    public IReadOnlyList<PropertyValue> Array { get; init; } = [];
}
