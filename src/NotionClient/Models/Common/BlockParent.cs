// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// Indicates that a Notion object is nested as a child block inside another block.
/// </summary>
public sealed class BlockParent : Parent
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "block_id";

    /// <summary>Gets the ID of the parent block.</summary>
    [JsonPropertyName("block_id")]
    public required string BlockId { get; init; }
}
