// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Common;

/// <summary>
/// Indicates that a Notion object (page or database) lives at the top level of a workspace,
/// with no page or database as a direct parent.
/// </summary>
public sealed class WorkspaceParent : Parent
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "workspace";

    /// <summary>Always <see langword="true"/>; signals workspace-level placement to the Notion API.</summary>
    [JsonPropertyName("workspace")]
    public bool Workspace { get; init; } = true;
}
