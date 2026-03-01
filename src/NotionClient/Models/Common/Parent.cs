// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// The parent of a Notion object (page, block, database, or workspace).
/// Discriminated union via the "type" field.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(DatabaseParent), "database_id")]
[JsonDerivedType(typeof(PageParent), "page_id")]
[JsonDerivedType(typeof(BlockParent), "block_id")]
[JsonDerivedType(typeof(WorkspaceParent), "workspace")]
[JsonDerivedType(typeof(DataSourceParent), "data_source_id")]
public abstract class Parent
{
    // Public parameterless constructor required by STJ polymorphic deserialization.
    /// <summary>Initializes a new instance of <see cref="Parent"/>.</summary>
    public Parent() { }

    /// <summary>Gets the parent type discriminator. Concrete subtypes return a fixed value such as "database_id", "page_id", "block_id", "workspace", or "data_source_id".</summary>
    [JsonIgnore]
    public virtual string Type => string.Empty;
}
