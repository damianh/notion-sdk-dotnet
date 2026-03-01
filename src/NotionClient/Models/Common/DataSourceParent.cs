// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// Parent for objects whose parent is a data source (externally synced).
/// </summary>
public sealed class DataSourceParent : Parent
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "data_source_id";

    /// <summary>Gets the ID of the external data source that owns this object.</summary>
    [JsonPropertyName("data_source_id")]
    public required string DataSourceId { get; init; }

    /// <summary>Gets the ID of the Notion database that mirrors the external data source.</summary>
    [JsonPropertyName("database_id")]
    public required string DatabaseId { get; init; }
}
