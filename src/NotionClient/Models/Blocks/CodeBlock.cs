// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion code block that displays a syntax-highlighted code snippet with a specified programming language.
/// </summary>
public sealed class CodeBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "code";

    /// <summary>Gets the code block content including the code text, caption, and language identifier.</summary>
    [JsonPropertyName("code")]
    public CodeContent Code { get; init; } = null!;
}
