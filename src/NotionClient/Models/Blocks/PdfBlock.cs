// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion PDF block that embeds a PDF file hosted internally by Notion or at an external URL.
/// </summary>
public sealed class PdfBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "pdf";

    /// <summary>Gets the PDF content including the file source and optional caption.</summary>
    [JsonPropertyName("pdf")]
    public InternalMediaContent Pdf { get; init; } = null!;
}
