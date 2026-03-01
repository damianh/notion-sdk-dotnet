// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>A file reference within a page property value (internal or external).</summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(InternalFileReference), "file")]
[JsonDerivedType(typeof(ExternalFileReference), "external")]
public abstract class FileReference
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    public FileReference() { }

    /// <summary>The file reference type discriminator: "file" (internal) or "external".</summary>
    [JsonIgnore]
    public virtual string FileRefType => string.Empty;

    /// <summary>The optional display name of the file.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
