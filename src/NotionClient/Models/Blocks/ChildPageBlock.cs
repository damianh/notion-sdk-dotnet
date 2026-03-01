// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion child page block that represents a sub-page nested directly within another page.
/// </summary>
public sealed class ChildPageBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "child_page";

    /// <summary>Gets the child page metadata, containing the title of the nested page.</summary>
    [JsonPropertyName("child_page")]
    public TitleContent ChildPage { get; init; } = null!;
}
