// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A template mention placeholder that resolves to the current user ("me") at page-creation time.
/// </summary>
public sealed class TemplateMentionUser : TemplateMentionContent
{
    /// <summary>Always "me"</summary>
    [JsonPropertyName("template_mention_user")]
    public string TemplateMentionUserValue { get; init; } = "me";
}
