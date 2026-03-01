// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Filter conditions for <c>date</c>, <c>created_time</c>, and <c>last_edited_time</c> properties.
/// Date values are ISO 8601 strings (e.g. <c>"2024-01-01"</c> or <c>"2024-01-01T00:00:00Z"</c>).
/// Relative-range conditions (e.g. <see cref="PastWeek"/>) should be set to an empty <see cref="object"/> (<c>{}</c>).
/// Only one condition should be set per filter.
/// </summary>
public sealed class DateFilterCondition
{
    /// <summary>Gets the ISO 8601 date string that the property value must equal exactly.</summary>
    [JsonPropertyName("equals")]
    public string? EqualsValue { get; init; }

    /// <summary>Gets the ISO 8601 date string that the property value must be earlier than (exclusive).</summary>
    [JsonPropertyName("before")]
    public string? Before { get; init; }

    /// <summary>Gets the ISO 8601 date string that the property value must be later than (exclusive).</summary>
    [JsonPropertyName("after")]
    public string? After { get; init; }

    /// <summary>Gets the ISO 8601 date string that the property value must be on or earlier than (inclusive).</summary>
    [JsonPropertyName("on_or_before")]
    public string? OnOrBefore { get; init; }

    /// <summary>Gets the ISO 8601 date string that the property value must be on or later than (inclusive).</summary>
    [JsonPropertyName("on_or_after")]
    public string? OnOrAfter { get; init; }

    /// <summary>Gets a value indicating whether the date property must be empty (no value assigned).</summary>
    [JsonPropertyName("is_empty")]
    public bool? IsEmpty { get; init; }

    /// <summary>Gets a value indicating whether the date property must have a value assigned.</summary>
    [JsonPropertyName("is_not_empty")]
    public bool? IsNotEmpty { get; init; }

    /// <summary>Gets a relative-range condition that matches dates within the past 7 days. Set to an empty object (<c>{}</c>) to activate.</summary>
    [JsonPropertyName("past_week")]
    public object? PastWeek { get; init; }

    /// <summary>Gets a relative-range condition that matches dates within the past 30 days. Set to an empty object (<c>{}</c>) to activate.</summary>
    [JsonPropertyName("past_month")]
    public object? PastMonth { get; init; }

    /// <summary>Gets a relative-range condition that matches dates within the past 365 days. Set to an empty object (<c>{}</c>) to activate.</summary>
    [JsonPropertyName("past_year")]
    public object? PastYear { get; init; }

    /// <summary>Gets a relative-range condition that matches dates within the next 7 days. Set to an empty object (<c>{}</c>) to activate.</summary>
    [JsonPropertyName("next_week")]
    public object? NextWeek { get; init; }

    /// <summary>Gets a relative-range condition that matches dates within the next 30 days. Set to an empty object (<c>{}</c>) to activate.</summary>
    [JsonPropertyName("next_month")]
    public object? NextMonth { get; init; }

    /// <summary>Gets a relative-range condition that matches dates within the next 365 days. Set to an empty object (<c>{}</c>) to activate.</summary>
    [JsonPropertyName("next_year")]
    public object? NextYear { get; init; }

    /// <summary>Gets a relative-range condition that matches dates within the current calendar week. Set to an empty object (<c>{}</c>) to activate.</summary>
    [JsonPropertyName("this_week")]
    public object? ThisWeek { get; init; }
}
