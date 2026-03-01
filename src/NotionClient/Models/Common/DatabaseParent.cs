// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// Indicates that a Notion object (page or block) is a row or child inside a database.
/// </summary>
public sealed class DatabaseParent : Parent
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "database_id";

    /// <summary>Gets the ID of the parent database.</summary>
    [JsonPropertyName("database_id")]
    public required string DatabaseId { get; init; }
}
