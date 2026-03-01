// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A <see cref="LinkToPageContent"/> variant that links to a Notion page by its unique identifier.
/// </summary>
public sealed class PageLinkToPage : LinkToPageContent
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string LinkType => "page_id";

    /// <summary>Gets the unique identifier of the Notion page that this block links to.</summary>
    [JsonPropertyName("page_id")]
    public string PageId { get; init; } = null!;
}
