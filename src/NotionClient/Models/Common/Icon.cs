// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// Icon on a Notion page or database. Can be an emoji, an internal/external file, or a custom emoji.
/// Discriminated union via the "type" field.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(EmojiIcon), "emoji")]
[JsonDerivedType(typeof(ExternalIcon), "external")]
[JsonDerivedType(typeof(FileIcon), "file")]
[JsonDerivedType(typeof(CustomEmojiIcon), "custom_emoji")]
public abstract class Icon
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    /// <summary>Initializes a new instance of <see cref="Icon"/>.</summary>
    public Icon() { }

    /// <summary>Gets the icon type discriminator. Concrete subtypes return a fixed value such as "emoji", "external", "file", or "custom_emoji".</summary>
    [JsonIgnore]
    public virtual string Type => string.Empty;
}
