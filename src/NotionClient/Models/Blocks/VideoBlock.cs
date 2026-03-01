// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion video block that embeds a video hosted internally by Notion or at an external URL (e.g. YouTube, Vimeo).
/// </summary>
public sealed class VideoBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "video";

    /// <summary>Gets the video content including the file source and optional caption.</summary>
    [JsonPropertyName("video")]
    public InternalMediaContent Video { get; init; } = null!;
}
