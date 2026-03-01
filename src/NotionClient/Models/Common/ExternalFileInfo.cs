// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>An external (publicly accessible) file reference.</summary>
public sealed class ExternalFileInfo
{
    /// <summary>Gets the publicly accessible URL of the externally-hosted file.</summary>
    [JsonPropertyName("url")]
    public required string Url { get; init; }
}
