// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// A rollup result containing an aggregated numeric value (e.g., sum, average, count).
/// </summary>
public sealed class NumberRollupResult : RollupResult
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string RollupType => "number";

    /// <summary>The aggregated numeric value, or <c>null</c> if the rollup produced no result.</summary>
    [JsonPropertyName("number")]
    public double? Number { get; init; }
}
