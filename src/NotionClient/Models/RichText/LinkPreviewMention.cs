// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A mention that embeds a link preview card (bookmark-style) inline in rich text.
/// </summary>
public sealed class LinkPreviewMention : Mention
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string MentionType => "link_preview";

    /// <summary>The URL information for this link preview mention.</summary>
    [JsonPropertyName("link_preview")]
    public required LinkPreviewMentionInfo LinkPreview { get; init; }
}
