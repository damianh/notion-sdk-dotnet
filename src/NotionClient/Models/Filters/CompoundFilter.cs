// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Combines multiple filters with AND or OR logic.
/// Serialize as <c>{ "and": [...] }</c> or <c>{ "or": [...] }</c>.
/// </summary>
public sealed class CompoundFilter : Filter
{
    /// <summary>Gets the list of filters that must all be satisfied (logical AND).</summary>
    [JsonPropertyName("and")]
    public IReadOnlyList<Filter>? And { get; init; }

    /// <summary>Gets the list of filters where at least one must be satisfied (logical OR).</summary>
    [JsonPropertyName("or")]
    public IReadOnlyList<Filter>? Or { get; init; }
}
