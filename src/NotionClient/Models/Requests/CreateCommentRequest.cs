// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for the <see href="https://developers.notion.com/reference/create-a-comment">Create a comment</see> API endpoint.
/// Creates a new comment on a page or in an existing discussion thread.
/// </summary>
public sealed class CreateCommentRequest
{
    /// <summary>Gets the parent page on which to create the comment. Required when starting a new discussion; mutually exclusive with <see cref="DiscussionId"/>.</summary>
    [JsonPropertyName("parent")]
    public Parent? Parent { get; init; }

    /// <summary>Gets the ID of an existing discussion thread to reply to. Mutually exclusive with <see cref="Parent"/>.</summary>
    [JsonPropertyName("discussion_id")]
    public string? DiscussionId { get; init; }

    /// <summary>Gets the body of the comment as a list of rich text segments.</summary>
    [JsonPropertyName("rich_text")]
    public required IReadOnlyList<RichTextItem> RichText { get; init; }
}
