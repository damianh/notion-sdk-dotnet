// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>unique_id</c> property, which auto-increments a numeric ID for each page
/// in the database, with an optional prefix.
/// <see href="https://developers.notion.com/reference/property-object#unique-id"/>
/// </summary>
public sealed class UniqueIdPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "unique_id";

    /// <summary>The unique ID configuration including the optional prefix.</summary>
    [JsonPropertyName("unique_id")]
    public UniqueIdConfig? UniqueId { get; init; }
}
