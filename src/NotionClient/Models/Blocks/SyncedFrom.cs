// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>Identifies the original <see cref="SyncedBlock"/> that a synced copy references.</summary>
public sealed class SyncedFrom
{
    /// <summary>Gets the reference type discriminator, always <c>"block_id"</c>.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = "block_id";

    /// <summary>Gets the unique ID of the original synced block whose content is mirrored by this copy.</summary>
    [JsonPropertyName("block_id")]
    public string BlockId { get; init; } = null!;
}
