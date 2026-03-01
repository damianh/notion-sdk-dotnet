// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>The content payload of a <see cref="TableOfContentsBlock"/>.</summary>
public sealed class TableOfContentsContent
{
    /// <summary>Gets the text or background color applied to the table of contents block.</summary>
    [JsonPropertyName("color")]
    public ApiColor Color { get; init; } = ApiColor.Default;
}
