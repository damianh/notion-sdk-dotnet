// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Blocks;

/// <summary>
/// A <see cref="LinkToPageContent"/> variant that links to a Notion database by its unique identifier.
/// </summary>
public sealed class DatabaseLinkToPage : LinkToPageContent
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string LinkType => "database_id";

    /// <summary>Gets the unique identifier of the Notion database that this block links to.</summary>
    [JsonPropertyName("database_id")]
    public string DatabaseId { get; init; } = null!;
}
