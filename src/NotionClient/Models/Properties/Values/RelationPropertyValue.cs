// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>relation</c> property, containing references to related pages.
/// <see href="https://developers.notion.com/reference/property-value-object#relation-property-values"/>
/// </summary>
public sealed class RelationPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "relation";

    /// <summary>The list of page references linked by this relation property.</summary>
    [JsonPropertyName("relation")]
    public IReadOnlyList<ObjectReference> Relation { get; init; } = [];

    /// <summary>Only present if the relation has more items than returned in this response.</summary>
    [JsonPropertyName("has_more")]
    public bool HasMore { get; init; }
}
