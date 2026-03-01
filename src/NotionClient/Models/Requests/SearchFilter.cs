// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Restricts a search query to either pages or databases only.
/// </summary>
public sealed class SearchFilter
{
    /// <summary>Gets the object type to restrict results to; either <c>"page"</c> or <c>"database"</c>.</summary>
    [JsonPropertyName("value")]
    public required string Value { get; init; }   // "page" | "database"

    /// <summary>Gets the object property to filter on; always <c>"object"</c>.</summary>
    [JsonPropertyName("property")]
    public string Property { get; init; } = "object";
}
