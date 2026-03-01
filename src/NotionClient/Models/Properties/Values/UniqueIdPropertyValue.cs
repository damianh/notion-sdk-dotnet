// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>unique_id</c> property, containing an auto-incremented identifier
/// with an optional prefix.
/// <see href="https://developers.notion.com/reference/property-value-object#unique-id-property-values"/>
/// </summary>
public sealed class UniqueIdPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "unique_id";

    /// <summary>The unique ID value containing the sequential number and optional prefix.</summary>
    [JsonPropertyName("unique_id")]
    public UniqueIdValue UniqueId { get; init; } = null!;
}
