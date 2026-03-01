// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Filter conditions for a <c>checkbox</c> database property.
/// Only one condition should be set per filter.
/// </summary>
public sealed class CheckboxFilterCondition
{
    /// <summary>Gets the boolean value that the checkbox property must equal.</summary>
    [JsonPropertyName("equals")]
    public bool? EqualsValue { get; init; }

    /// <summary>Gets the boolean value that the checkbox property must not equal.</summary>
    [JsonPropertyName("does_not_equal")]
    public bool? DoesNotEqual { get; init; }
}
