// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Filter conditions for a <c>select</c> database property.
/// Only one condition should be set per filter.
/// </summary>
public sealed class SelectFilterCondition
{
    /// <summary>Gets the option name that the property value must equal exactly.</summary>
    [JsonPropertyName("equals")]
    public string? EqualsValue { get; init; }

    /// <summary>Gets the option name that the property value must not equal.</summary>
    [JsonPropertyName("does_not_equal")]
    public string? DoesNotEqual { get; init; }

    /// <summary>Gets a value indicating whether the select property must have no option selected.</summary>
    [JsonPropertyName("is_empty")]
    public bool? IsEmpty { get; init; }

    /// <summary>Gets a value indicating whether the select property must have an option selected.</summary>
    [JsonPropertyName("is_not_empty")]
    public bool? IsNotEmpty { get; init; }
}
