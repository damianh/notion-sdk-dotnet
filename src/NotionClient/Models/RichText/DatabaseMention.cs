// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A mention that references a Notion database inline in rich text.
/// </summary>
public sealed class DatabaseMention : Mention
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string MentionType => "database";

    /// <summary>A reference to the mentioned Notion database.</summary>
    [JsonPropertyName("database")]
    public required ObjectReference Database { get; init; }
}
