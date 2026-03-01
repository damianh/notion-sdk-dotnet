// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>A minimal content model that carries only a plain-text title string, used by <see cref="ChildPageBlock"/> and <see cref="ChildDatabaseBlock"/>.</summary>
public sealed class TitleContent
{
    /// <summary>Gets the plain-text title of the child page or database.</summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = null!;
}
