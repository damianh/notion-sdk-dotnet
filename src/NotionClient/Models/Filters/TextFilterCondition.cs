// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Filter conditions for text-based database properties: <c>title</c>, <c>rich_text</c>, <c>url</c>,
/// <c>email</c>, and <c>phone_number</c>. Also used for <c>formula</c> properties that return a string.
/// Only one condition should be set per filter.
/// </summary>
public sealed class TextFilterCondition
{
    /// <summary>Gets the string that the property value must equal exactly (case-sensitive).</summary>
    [JsonPropertyName("equals")]
    public string? EqualsValue { get; init; }

    /// <summary>Gets the string that the property value must not equal.</summary>
    [JsonPropertyName("does_not_equal")]
    public string? DoesNotEqual { get; init; }

    /// <summary>Gets the substring that must appear anywhere in the property value.</summary>
    [JsonPropertyName("contains")]
    public string? Contains { get; init; }

    /// <summary>Gets the substring that must not appear anywhere in the property value.</summary>
    [JsonPropertyName("does_not_contain")]
    public string? DoesNotContain { get; init; }

    /// <summary>Gets the prefix string that the property value must begin with.</summary>
    [JsonPropertyName("starts_with")]
    public string? StartsWith { get; init; }

    /// <summary>Gets the suffix string that the property value must end with.</summary>
    [JsonPropertyName("ends_with")]
    public string? EndsWith { get; init; }

    /// <summary>Gets a value indicating whether the text property must be empty.</summary>
    [JsonPropertyName("is_empty")]
    public bool? IsEmpty { get; init; }

    /// <summary>Gets a value indicating whether the text property must have a non-empty value.</summary>
    [JsonPropertyName("is_not_empty")]
    public bool? IsNotEmpty { get; init; }
}
