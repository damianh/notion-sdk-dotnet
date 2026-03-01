// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// An inline hyperlink attached to a text rich text segment.
/// </summary>
public sealed class TextLink
{
    /// <summary>The URL that this text segment links to.</summary>
    [JsonPropertyName("url")]
    public required string Url { get; init; }
}
