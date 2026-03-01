// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion toggle block that hides or reveals its child blocks when the user clicks the toggle arrow.
/// </summary>
public sealed class ToggleBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "toggle";

    /// <summary>Gets the toggle content including the summary rich text and the child blocks shown when expanded.</summary>
    [JsonPropertyName("toggle")]
    public RichTextWithColorAndChildren Toggle { get; init; } = null!;
}
