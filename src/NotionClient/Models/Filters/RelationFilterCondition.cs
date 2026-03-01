// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Filter conditions for a <c>relation</c> database property, which links to pages in another database.
/// Only one condition should be set per filter.
/// </summary>
public sealed class RelationFilterCondition
{
    /// <summary>Gets the page ID that must be present among the related pages.</summary>
    [JsonPropertyName("contains")]
    public string? Contains { get; init; }

    /// <summary>Gets the page ID that must not be present among the related pages.</summary>
    [JsonPropertyName("does_not_contain")]
    public string? DoesNotContain { get; init; }

    /// <summary>Gets a value indicating whether the relation property must have no linked pages.</summary>
    [JsonPropertyName("is_empty")]
    public bool? IsEmpty { get; init; }

    /// <summary>Gets a value indicating whether the relation property must have at least one linked page.</summary>
    [JsonPropertyName("is_not_empty")]
    public bool? IsNotEmpty { get; init; }
}
