// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>The content payload of a <see cref="CodeBlock"/>.</summary>
public sealed class CodeContent
{
    /// <summary>Gets the rich-text items containing the code snippet text.</summary>
    [JsonPropertyName("rich_text")]
    public IReadOnlyList<RichTextItem> RichText { get; init; } = [];

    /// <summary>Gets the optional rich-text caption displayed beneath the code block.</summary>
    [JsonPropertyName("caption")]
    public IReadOnlyList<RichTextItem> Caption { get; init; } = [];

    /// <summary>Gets the programming or markup language name used for syntax highlighting (e.g. <c>"javascript"</c>, <c>"python"</c>).</summary>
    [JsonPropertyName("language")]
    public string Language { get; init; } = "plain text";
}
