// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion link preview block that renders a rich preview card for an external URL (e.g. a GitHub PR or Jira ticket)
/// using a Notion integration connection.
/// </summary>
public sealed class LinkPreviewBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "link_preview";

    /// <summary>Gets the link preview content containing the external URL to preview.</summary>
    [JsonPropertyName("link_preview")]
    public LinkPreviewContent LinkPreview { get; init; } = null!;
}
