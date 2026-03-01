// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Schema;

/// <summary>
/// Schema for a Notion <c>files</c> property, which stores one or more file or image attachments.
/// <see href="https://developers.notion.com/reference/property-object#files"/>
/// </summary>
public sealed class FilesPropertySchema : PropertySchema
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "files";

    /// <summary>The files configuration object (currently empty in the Notion API).</summary>
    [JsonPropertyName("files")]
    public object? Files { get; init; }
}
