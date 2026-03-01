// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Filter conditions for a <c>number</c> database property.
/// Only one condition should be set per filter.
/// </summary>
public sealed class NumberFilterCondition
{
    /// <summary>Gets the number that the property value must equal exactly.</summary>
    [JsonPropertyName("equals")]
    public double? EqualsValue { get; init; }

    /// <summary>Gets the number that the property value must not equal.</summary>
    [JsonPropertyName("does_not_equal")]
    public double? DoesNotEqual { get; init; }

    /// <summary>Gets the lower bound (exclusive) for the numeric comparison.</summary>
    [JsonPropertyName("greater_than")]
    public double? GreaterThan { get; init; }

    /// <summary>Gets the upper bound (exclusive) for the numeric comparison.</summary>
    [JsonPropertyName("less_than")]
    public double? LessThan { get; init; }

    /// <summary>Gets the lower bound (inclusive) for the numeric comparison.</summary>
    [JsonPropertyName("greater_than_or_equal_to")]
    public double? GreaterThanOrEqualTo { get; init; }

    /// <summary>Gets the upper bound (inclusive) for the numeric comparison.</summary>
    [JsonPropertyName("less_than_or_equal_to")]
    public double? LessThanOrEqualTo { get; init; }

    /// <summary>Gets a value indicating whether the number property must be empty (no value assigned).</summary>
    [JsonPropertyName("is_empty")]
    public bool? IsEmpty { get; init; }

    /// <summary>Gets a value indicating whether the number property must have a value assigned.</summary>
    [JsonPropertyName("is_not_empty")]
    public bool? IsNotEmpty { get; init; }
}
