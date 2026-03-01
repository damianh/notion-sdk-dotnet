// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion to-do block representing a single checkbox task item that can be checked or unchecked.
/// </summary>
public sealed class ToDoBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "to_do";

    /// <summary>Gets the to-do content including rich text, color, and the checked state of the task.</summary>
    [JsonPropertyName("to_do")]
    public ToDoContent ToDo { get; init; } = null!;
}
