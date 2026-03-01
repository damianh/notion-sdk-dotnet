// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Request body for the <see href="https://developers.notion.com/reference/update-a-block">Update a block</see> API endpoint.
/// The update payload shape varies by block type; this type uses extension data to allow
/// any block-type-specific fields to be passed through without a strongly-typed wrapper.
/// </summary>
public sealed class UpdateBlockRequest
{
    // The update shape depends on the block type; we send the full block delta.
    // Using a flexible dictionary to allow any block-type-specific fields.
    /// <summary>Gets the block-type-specific update fields as raw key/value pairs that are merged into the serialized JSON payload.</summary>
    [JsonExtensionData]
    public IDictionary<string, object?>? Extensions { get; init; }
}
