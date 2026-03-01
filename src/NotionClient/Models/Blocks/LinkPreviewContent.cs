// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>The content payload of a <see cref="LinkPreviewBlock"/>.</summary>
public sealed class LinkPreviewContent
{
    /// <summary>Gets the URL of the external resource to render as a rich preview card.</summary>
    [JsonPropertyName("url")]
    public string Url { get; init; } = null!;
}
