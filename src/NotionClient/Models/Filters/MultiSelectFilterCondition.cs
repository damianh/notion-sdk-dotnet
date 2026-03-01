// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Filter conditions for a <c>multi_select</c> database property.
/// Only one condition should be set per filter.
/// </summary>
public sealed class MultiSelectFilterCondition
{
    /// <summary>Gets the option name that must be present among the selected values.</summary>
    [JsonPropertyName("contains")]
    public string? Contains { get; init; }

    /// <summary>Gets the option name that must not be present among the selected values.</summary>
    [JsonPropertyName("does_not_contain")]
    public string? DoesNotContain { get; init; }

    /// <summary>Gets a value indicating whether the multi-select property must have no options selected.</summary>
    [JsonPropertyName("is_empty")]
    public bool? IsEmpty { get; init; }

    /// <summary>Gets a value indicating whether the multi-select property must have at least one option selected.</summary>
    [JsonPropertyName("is_not_empty")]
    public bool? IsNotEmpty { get; init; }
}
