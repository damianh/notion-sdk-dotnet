// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models;

/// <summary>
/// A Notion comment on a page or discussion thread.
/// </summary>
/// <remarks>
/// See <see href="https://developers.notion.com/reference/comment-object">Notion API — Comment</see>.
/// </remarks>
public sealed class Comment
{
    /// <summary>Gets the Notion object type, always <c>"comment"</c>.</summary>
    [JsonPropertyName("object")]
    public string Object { get; init; } = "comment";

    /// <summary>Gets the unique identifier (UUID) of the comment.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets the parent of this comment (a page or block).</summary>
    [JsonPropertyName("parent")]
    public Parent? Parent { get; init; }

    /// <summary>Gets the identifier of the discussion thread this comment belongs to.</summary>
    [JsonPropertyName("discussion_id")]
    public string? DiscussionId { get; init; }

    /// <summary>Gets the ISO 8601 timestamp when the comment was created.</summary>
    [JsonPropertyName("created_time")]
    public string? CreatedTime { get; init; }

    /// <summary>Gets the user who created the comment.</summary>
    [JsonPropertyName("created_by")]
    public PartialUser? CreatedBy { get; init; }

    /// <summary>Gets the ISO 8601 timestamp of the most recent edit to the comment.</summary>
    [JsonPropertyName("last_edited_time")]
    public string? LastEditedTime { get; init; }

    /// <summary>Gets the rich text content of the comment.</summary>
    [JsonPropertyName("rich_text")]
    public IReadOnlyList<RichTextItem> RichText { get; init; } = [];
}
