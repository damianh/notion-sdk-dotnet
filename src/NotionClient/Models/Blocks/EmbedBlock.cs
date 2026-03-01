// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion embed block that renders external content (e.g. a tweet, Figma frame, or other embeddable URL) inline on a page.
/// </summary>
public sealed class EmbedBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "embed";

    /// <summary>Gets the embed content containing the URL of the embedded external resource and an optional caption.</summary>
    [JsonPropertyName("embed")]
    public UrlContent Embed { get; init; } = null!;
}
