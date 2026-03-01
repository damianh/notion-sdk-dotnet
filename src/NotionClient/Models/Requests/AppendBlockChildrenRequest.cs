// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.Blocks;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for the <see href="https://developers.notion.com/reference/patch-block-children">Append block children</see> API endpoint.
/// Appends new block content after existing children of a page or block.
/// </summary>
public sealed class AppendBlockChildrenRequest
{
    /// <summary>Gets the list of block objects to append as children of the target block or page.</summary>
    [JsonPropertyName("children")]
    public required IReadOnlyList<Block> Children { get; init; }

    /// <summary>Gets the ID of an existing block after which the new children should be inserted, or <see langword="null"/> to append at the end.</summary>
    [JsonPropertyName("after")]
    public string? After { get; init; }
}
