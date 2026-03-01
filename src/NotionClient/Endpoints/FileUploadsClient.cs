// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

internal sealed class FileUploadsClient : IFileUploadsClient
{
    private readonly NotionClient _client;

    internal FileUploadsClient(NotionClient client) => _client = client;

    public Task<FileUpload> Create(
        CreateFileUploadRequest? request = null,
        CancellationToken cancellationToken = default)
        => _client.Send<FileUpload>(
            HttpMethod.Post,
            "file_uploads",
            body: request,
            cancellationToken: cancellationToken);

    public Task<FileUpload> Get(string fileUploadId, CancellationToken cancellationToken = default)
        => _client.Send<FileUpload>(
            HttpMethod.Get,
            $"file_uploads/{fileUploadId}",
            cancellationToken: cancellationToken);

    public async Task<FileUpload> SendPart(
        string fileUploadId,
        Stream content,
        string contentType,
        int? partNumber = null,
        CancellationToken cancellationToken = default)
    {
        var multipart = new MultipartFormDataContent();
        var streamContent = new StreamContent(content);
        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
        multipart.Add(streamContent, "file", "upload");

        return await _client.SendMultipart<FileUpload>(
            $"file_uploads/{fileUploadId}/send",
            multipart,
            partNumber.HasValue
                ? new Dictionary<string, string?> { ["part_number"] = partNumber.Value.ToString() }
                : null,
            cancellationToken);
    }

    public Task<FileUpload> Complete(string fileUploadId, CancellationToken cancellationToken = default)
        => _client.Send<FileUpload>(
            HttpMethod.Post,
            $"file_uploads/{fileUploadId}/complete",
            cancellationToken: cancellationToken);
}
