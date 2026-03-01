// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Properties.Values;

/// <summary>
/// Property value for a Notion <c>files</c> property, containing a list of file or image attachments.
/// <see href="https://developers.notion.com/reference/property-value-object#files-property-values"/>
/// </summary>
public sealed class FilesPropertyValue : PropertyValue
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "files";

    /// <summary>The list of file references (internal or external) attached to this property.</summary>
    [JsonPropertyName("files")]
    public IReadOnlyList<FileReference> Files { get; init; } = [];
}
