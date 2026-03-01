// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Filter conditions for <c>people</c>, <c>created_by</c>, and <c>last_edited_by</c> database properties,
/// which reference Notion user IDs. Only one condition should be set per filter.
/// </summary>
public sealed class PeopleFilterCondition
{
    /// <summary>Gets the user ID that must be present among the selected people.</summary>
    [JsonPropertyName("contains")]
    public string? Contains { get; init; }

    /// <summary>Gets the user ID that must not be present among the selected people.</summary>
    [JsonPropertyName("does_not_contain")]
    public string? DoesNotContain { get; init; }

    /// <summary>Gets a value indicating whether the people property must have no users selected.</summary>
    [JsonPropertyName("is_empty")]
    public bool? IsEmpty { get; init; }

    /// <summary>Gets a value indicating whether the people property must have at least one user selected.</summary>
    [JsonPropertyName("is_not_empty")]
    public bool? IsNotEmpty { get; init; }
}
