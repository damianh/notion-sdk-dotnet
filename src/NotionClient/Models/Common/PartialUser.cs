// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// A minimal user reference — only contains the object type and ID.
/// Returned as created_by / last_edited_by on blocks and pages.
/// </summary>
public sealed class PartialUser
{
    /// <summary>Gets the Notion object type; always <c>"user"</c>.</summary>
    [JsonPropertyName("object")]
    public string Object { get; init; } = "user";

    /// <summary>Gets the unique identifier of the user.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}
