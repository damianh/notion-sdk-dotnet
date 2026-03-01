// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A <see cref="LinkToPageContent"/> variant that links to a Notion comment by its unique identifier.
/// </summary>
public sealed class CommentLinkToPage : LinkToPageContent
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string LinkType => "comment_id";

    /// <summary>Gets the unique identifier of the Notion comment that this block links to.</summary>
    [JsonPropertyName("comment_id")]
    public string CommentId { get; init; } = null!;
}
