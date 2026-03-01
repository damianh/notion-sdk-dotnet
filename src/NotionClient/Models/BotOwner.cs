// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models;

/// <summary>
/// Base class for the owner of a bot integration. Discriminated by <c>"type"</c> into
/// <see cref="WorkspaceBotOwner"/> (workspace-level) or <see cref="UserBotOwner"/> (user-level).
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(WorkspaceBotOwner), "workspace")]
[JsonDerivedType(typeof(UserBotOwner), "user")]
public abstract class BotOwner
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    protected BotOwner() { }

    /// <summary>Gets the owner type discriminator (e.g., <c>"workspace"</c> or <c>"user"</c>).</summary>
    [JsonIgnore]
    public virtual string OwnerType => string.Empty;
}
