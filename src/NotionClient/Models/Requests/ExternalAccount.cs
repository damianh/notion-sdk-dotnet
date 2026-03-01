// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Requests;

/// <summary>
/// Represents an external account associated with a Notion integration during the OAuth token exchange.
/// Used to link the Notion bot to a specific account in a third-party service.
/// </summary>
public sealed class ExternalAccount
{
    /// <summary>Gets the unique key identifying the external account within the third-party service.</summary>
    [JsonPropertyName("key")]
    public required string Key { get; init; }

    /// <summary>Gets the display name of the external account as it should appear in the Notion UI.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
