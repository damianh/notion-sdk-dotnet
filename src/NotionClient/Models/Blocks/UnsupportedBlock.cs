// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion unsupported block returned by the API for block types that are not yet exposed in the public API
/// or are not recognised by this SDK version.
/// </summary>
public sealed class UnsupportedBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "unsupported";

    /// <summary>Gets the raw unsupported block data object. The structure is opaque and may vary between block types.</summary>
    [JsonPropertyName("unsupported")]
    public object? Unsupported { get; init; }
}
