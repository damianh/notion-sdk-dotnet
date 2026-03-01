// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion child database block that represents an inline database nested within a page.
/// </summary>
public sealed class ChildDatabaseBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "child_database";

    /// <summary>Gets the child database metadata, containing the title of the nested database.</summary>
    [JsonPropertyName("child_database")]
    public TitleContent ChildDatabase { get; init; } = null!;
}
