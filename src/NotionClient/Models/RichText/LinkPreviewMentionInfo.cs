// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// URL data for a <c>link_preview</c> mention embedded in rich text.
/// </summary>
public sealed class LinkPreviewMentionInfo
{
    /// <summary>The URL of the previewed link.</summary>
    [JsonPropertyName("url")]
    public required string Url { get; init; }
}
