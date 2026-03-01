// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>Media block content backed by an internal Notion file.</summary>
public sealed class InternalMediaContent
{
    /// <summary>Gets the file source type, either <c>"file"</c> (Notion-hosted) or <c>"external"</c>.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = "file";

    /// <summary>Gets the metadata for a Notion-internally-hosted file, populated when <see cref="Type"/> is <c>"file"</c>.</summary>
    [JsonPropertyName("file")]
    public InternalFileInfo? File { get; init; }

    /// <summary>Gets the metadata for an externally-hosted file, populated when <see cref="Type"/> is <c>"external"</c>.</summary>
    [JsonPropertyName("external")]
    public ExternalFileInfo? External { get; init; }

    /// <summary>Gets the optional rich-text caption displayed beneath the media content.</summary>
    [JsonPropertyName("caption")]
    public IReadOnlyList<RichTextItem> Caption { get; init; } = [];
}
