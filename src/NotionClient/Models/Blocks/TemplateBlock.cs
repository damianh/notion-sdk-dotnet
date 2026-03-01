// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion template block that acts as a button users can click to instantly duplicate a set of child blocks.
/// </summary>
public sealed class TemplateBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "template";

    /// <summary>Gets the template content including the button label as rich text and the child blocks that will be duplicated on use.</summary>
    [JsonPropertyName("template")]
    public RichTextWithColorAndChildren Template { get; init; } = null!;
}
