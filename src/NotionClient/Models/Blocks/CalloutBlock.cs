// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion callout block that displays highlighted text with an optional icon, typically used to draw attention to important information.
/// </summary>
public sealed class CalloutBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "callout";

    /// <summary>Gets the callout content including rich text, color, and an optional icon.</summary>
    [JsonPropertyName("callout")]
    public CalloutContent Callout { get; init; } = null!;
}
