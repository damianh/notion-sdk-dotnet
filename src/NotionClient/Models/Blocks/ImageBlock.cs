// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion image block that displays an image hosted internally by Notion or at an external URL.
/// </summary>
public sealed class ImageBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "image";

    /// <summary>Gets the image content, including the file source and optional caption.</summary>
    [JsonPropertyName("image")]
    public InternalMediaContent Image { get; init; } = null!;
}
