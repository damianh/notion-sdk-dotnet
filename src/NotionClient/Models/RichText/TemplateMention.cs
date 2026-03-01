// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// A mention representing a template placeholder that is resolved when a page is created from a template.
/// The placeholder is either a date token ("today"/"now") or a user token ("me").
/// </summary>
public sealed class TemplateMention : Mention
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string MentionType => "template_mention";

    /// <summary>The template mention content specifying whether it resolves to a date or user value.</summary>
    [JsonPropertyName("template_mention")]
    public TemplateMentionContent? TemplateMentionData { get; init; }
}
