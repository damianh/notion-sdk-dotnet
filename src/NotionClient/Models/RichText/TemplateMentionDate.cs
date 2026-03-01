// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A template mention placeholder that resolves to a date at page-creation time.
/// </summary>
public sealed class TemplateMentionDate : TemplateMentionContent
{
    /// <summary>"today" or "now"</summary>
    [JsonPropertyName("template_mention_date")]
    public required string TemplateMentionDateValue { get; init; }
}
