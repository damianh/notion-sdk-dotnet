// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>The content payload of a <see cref="SyncedBlock"/>.</summary>
public sealed class SyncedBlockContent
{
    /// <summary>null when this is the original block (not a synced copy).</summary>
    [JsonPropertyName("synced_from")]
    public SyncedFrom? SyncedFrom { get; init; }
}
