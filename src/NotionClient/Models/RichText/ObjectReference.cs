// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.RichText;

/// <summary>A simple id-only reference to a Notion object (page or database).</summary>
public sealed class ObjectReference
{
    /// <summary>The unique identifier of the referenced Notion object.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}
