// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A mention that embeds a rich link preview (with title, description, and thumbnail metadata) inline in rich text.
/// </summary>
public sealed class LinkMention : Mention
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string MentionType => "link_mention";

    /// <summary>The metadata for the linked resource, including URL, title, description, and media URLs.</summary>
    [JsonPropertyName("link_mention")]
    public required LinkMentionInfo LinkMentionData { get; init; }
}
