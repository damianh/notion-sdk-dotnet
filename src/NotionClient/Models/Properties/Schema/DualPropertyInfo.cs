// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// References the mirrored property on the related database in a dual-property relation.
/// </summary>
public sealed class DualPropertyInfo
{
    /// <summary>The name of the corresponding synced property in the related database.</summary>
    [JsonPropertyName("synced_property_name")]
    public string? SyncedPropertyName { get; init; }

    /// <summary>The ID of the corresponding synced property in the related database.</summary>
    [JsonPropertyName("synced_property_id")]
    public string? SyncedPropertyId { get; init; }
}
