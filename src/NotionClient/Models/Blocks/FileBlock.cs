// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion file block that represents an uploaded or externally-linked file attachment on a page.
/// </summary>
public sealed class FileBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "file";

    /// <summary>Gets the file content including the file source, optional display name, and caption.</summary>
    [JsonPropertyName("file")]
    public InternalMediaContentWithName File { get; init; } = null!;
}
