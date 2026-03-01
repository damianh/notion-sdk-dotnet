// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A mention that references a Notion page inline in rich text.
/// </summary>
public sealed class PageMention : Mention
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string MentionType => "page";

    /// <summary>A reference to the mentioned Notion page.</summary>
    [JsonPropertyName("page")]
    public required ObjectReference Page { get; init; }
}
