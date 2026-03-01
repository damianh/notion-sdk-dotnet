// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models;

/// <summary>
/// A search result — either a <see cref="Page"/> or a <see cref="Database"/>.
/// Discriminated by the "object" field.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "object")]
[JsonDerivedType(typeof(PageSearchResult), "page")]
[JsonDerivedType(typeof(DatabaseSearchResult), "database")]
public abstract class SearchResult
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    protected SearchResult() { }

    /// <summary>Gets the search result type discriminator (e.g., <c>"page"</c> or <c>"database"</c>).</summary>
    [JsonIgnore]
    public virtual string ObjectType => string.Empty;

    /// <summary>Gets the unique identifier of the search result object (UUID).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}
