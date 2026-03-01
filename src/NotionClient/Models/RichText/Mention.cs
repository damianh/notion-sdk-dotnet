// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>
/// Abstract base class for an inline mention within a rich text item.
/// A mention refers to a Notion object or concept embedded in text.
/// Subtypes: <see cref="UserMention"/> (user), <see cref="DateMention"/> (date),
/// <see cref="PageMention"/> (page), <see cref="DatabaseMention"/> (database),
/// <see cref="LinkPreviewMention"/> (link_preview), <see cref="LinkMention"/> (link_mention),
/// <see cref="TemplateMention"/> (template_mention), <see cref="CustomEmojiMention"/> (custom_emoji).
/// <see href="https://developers.notion.com/reference/rich-text#mention"/>
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(UserMention), "user")]
[JsonDerivedType(typeof(DateMention), "date")]
[JsonDerivedType(typeof(PageMention), "page")]
[JsonDerivedType(typeof(DatabaseMention), "database")]
[JsonDerivedType(typeof(LinkPreviewMention), "link_preview")]
[JsonDerivedType(typeof(LinkMention), "link_mention")]
[JsonDerivedType(typeof(TemplateMention), "template_mention")]
[JsonDerivedType(typeof(CustomEmojiMention), "custom_emoji")]
public abstract class Mention
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public Mention() { }

    /// <summary>The mention type discriminator string (e.g., "user", "page", "database").</summary>
    [JsonIgnore]
    public virtual string MentionType => string.Empty;
}
