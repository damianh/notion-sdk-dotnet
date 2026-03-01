// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A Notion breadcrumb block that displays the navigational path from the workspace root to the current page.
/// </summary>
public sealed class BreadcrumbBlock : Block
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "breadcrumb";

    /// <summary>Gets the breadcrumb data object. This is always an empty object in the Notion API.</summary>
    [JsonPropertyName("breadcrumb")]
    public object? Breadcrumb { get; init; }
}
