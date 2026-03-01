// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Common;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A mention that references a Notion user inline in rich text.
/// </summary>
public sealed class UserMention : Mention
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string MentionType => "user";

    /// <summary>The referenced Notion user.</summary>
    [JsonPropertyName("user")]
    public required PartialUser User { get; init; }
}
