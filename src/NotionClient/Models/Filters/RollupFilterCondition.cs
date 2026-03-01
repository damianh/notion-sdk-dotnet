// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Filter conditions for a <c>rollup</c> database property.
/// Rollup filters can match when <em>any</em>, <em>every</em>, or <em>none</em> of the rolled-up entries satisfy
/// a given <see cref="PropertyFilter"/>, or they can apply numeric/date conditions to the aggregated value.
/// Only one condition should be set per filter.
/// </summary>
public sealed class RollupFilterCondition
{
    /// <summary>Gets a property filter that must match at least one item in the rollup's related entries.</summary>
    [JsonPropertyName("any")]
    public PropertyFilter? Any { get; init; }

    /// <summary>Gets a property filter that must match every item in the rollup's related entries.</summary>
    [JsonPropertyName("every")]
    public PropertyFilter? Every { get; init; }

    /// <summary>Gets a property filter that must not match any item in the rollup's related entries.</summary>
    [JsonPropertyName("none")]
    public PropertyFilter? None { get; init; }

    /// <summary>Gets a numeric filter condition applied to the aggregated rollup number value.</summary>
    [JsonPropertyName("number")]
    public NumberFilterCondition? Number { get; init; }

    /// <summary>Gets a date filter condition applied to the aggregated rollup date value.</summary>
    [JsonPropertyName("date")]
    public DateFilterCondition? Date { get; init; }
}
