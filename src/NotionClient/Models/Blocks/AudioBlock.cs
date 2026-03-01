// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion audio block that embeds an audio file hosted internally by Notion or at an external URL.
/// </summary>
public sealed class AudioBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "audio";

    /// <summary>Gets the audio file content, including the file source and optional caption.</summary>
    [JsonPropertyName("audio")]
    public InternalMediaContent Audio { get; init; } = null!;
}
