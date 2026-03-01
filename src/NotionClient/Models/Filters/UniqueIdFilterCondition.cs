// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Filter conditions for a <c>unique_id</c> database property, which stores an auto-incrementing integer identifier.
/// Only one condition should be set per filter.
/// </summary>
public sealed class UniqueIdFilterCondition
{
    /// <summary>Gets the unique ID value that must be matched exactly.</summary>
    [JsonPropertyName("equals")]
    public int? EqualsValue { get; init; }

    /// <summary>Gets the unique ID value that must not be matched.</summary>
    [JsonPropertyName("does_not_equal")]
    public int? DoesNotEqual { get; init; }

    /// <summary>Gets the lower bound (exclusive) for the unique ID comparison.</summary>
    [JsonPropertyName("greater_than")]
    public int? GreaterThan { get; init; }

    /// <summary>Gets the upper bound (exclusive) for the unique ID comparison.</summary>
    [JsonPropertyName("less_than")]
    public int? LessThan { get; init; }

    /// <summary>Gets the lower bound (inclusive) for the unique ID comparison.</summary>
    [JsonPropertyName("greater_than_or_equal_to")]
    public int? GreaterThanOrEqualTo { get; init; }

    /// <summary>Gets the upper bound (inclusive) for the unique ID comparison.</summary>
    [JsonPropertyName("less_than_or_equal_to")]
    public int? LessThanOrEqualTo { get; init; }

    /// <summary>Gets a value indicating whether the property must be empty (no value assigned).</summary>
    [JsonPropertyName("is_empty")]
    public bool? IsEmpty { get; init; }

    /// <summary>Gets a value indicating whether the property must have a value assigned.</summary>
    [JsonPropertyName("is_not_empty")]
    public bool? IsNotEmpty { get; init; }
}
