// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

/// <summary>
/// Upload and manage files that can be referenced by file-type blocks and page properties.
/// <see href="https://developers.notion.com/reference/file-upload"/>
/// </summary>
public interface IFileUploadsClient
{
    /// <summary>Initiates a new file upload and returns a pending <see cref="FileUpload"/> with an upload URL.</summary>
    Task<FileUpload> Create(CreateFileUploadRequest? request = null, CancellationToken cancellationToken = default);

    /// <summary>Retrieves the current status and metadata of a file upload.</summary>
    Task<FileUpload> Get(string fileUploadId, CancellationToken cancellationToken = default);

    /// <summary>Uploads a single part (or the entirety) of the file content.</summary>
    Task<FileUpload> SendPart(string fileUploadId, Stream content, string contentType, int? partNumber = null, CancellationToken cancellationToken = default);

    /// <summary>Marks the file upload as complete so it can be referenced by blocks and properties.</summary>
    Task<FileUpload> Complete(string fileUploadId, CancellationToken cancellationToken = default);
}
