// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;

namespace DamianH.NotionClient;

/// <summary>
/// Provides the canonical <see cref="JsonSerializerOptions"/> used by the Notion client
/// for serializing and deserializing Notion API payloads.
/// </summary>
public static class NotionJsonSerializerOptions
{
    /// <summary>
    /// The default options used by <see cref="NotionClient"/>:
    /// snake_case property names, null values omitted, and source-generated type metadata.
    /// These are configured via <see cref="NotionJsonSerializerContext"/> source-generation attributes.
    /// </summary>
    public static JsonSerializerOptions Default => NotionJsonSerializerContext.Default.Options;
}
