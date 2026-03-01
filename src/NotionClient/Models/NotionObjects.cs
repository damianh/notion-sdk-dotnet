// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models;

/// <summary>
/// Base class for a page cover image. Discriminated by <c>"type"</c> into
/// <see cref="ExternalPageCover"/> (URL-based) or <see cref="FilePageCover"/> (Notion-hosted).
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ExternalPageCover), "external")]
[JsonDerivedType(typeof(FilePageCover), "file")]
public abstract class PageCover
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public PageCover() { }

    /// <summary>Gets the cover type discriminator (e.g., <c>"external"</c> or <c>"file"</c>).</summary>
    [JsonIgnore]
    public virtual string CoverType => string.Empty;
}

/// <summary>
/// A Notion user — either a person or a bot.
/// Discriminated by "type" field.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(PersonUser), "person")]
[JsonDerivedType(typeof(BotUser), "bot")]
public abstract class User
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public User() { }

    /// <summary>Gets the Notion object type, always <c>"user"</c>.</summary>
    [JsonPropertyName("object")]
    public string Object { get; init; } = "user";

    /// <summary>Gets the unique identifier of the user (UUID).</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets the display name of the user, if available.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Gets the URL of the user's avatar image, if set.</summary>
    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; init; }

    /// <summary>Gets the user type discriminator (e.g., <c>"person"</c> or <c>"bot"</c>).</summary>
    [JsonIgnore]
    public virtual string UserType => string.Empty;
}
