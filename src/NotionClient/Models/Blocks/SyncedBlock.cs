// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion synced block that allows the same content to appear on multiple pages.
/// The original block has a <c>null</c> <c>synced_from</c> value; copies reference the original block's ID.
/// </summary>
public sealed class SyncedBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "synced_block";

    /// <summary>Gets the synced block content, which identifies whether this is the original or a synced copy.</summary>
    [JsonPropertyName("synced_block")]
    public SyncedBlockContent SyncedBlockContent { get; init; } = null!;
}
