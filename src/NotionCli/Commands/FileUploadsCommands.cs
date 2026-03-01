// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.CommandLine;
using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Requests;
using DamianH.NotionCli.Auth;
using DamianH.NotionCli.Infrastructure;

namespace DamianH.NotionCli.Commands;

internal static class FileUploadsCommands
{
    internal static Command Build(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var fileUploadsCmd = new Command("file-uploads", "Upload and manage files that can be referenced by blocks and page properties.");
        fileUploadsCmd.Subcommands.Add(BuildCreate(tokenOption, verboseOption, noIndentOption));
        fileUploadsCmd.Subcommands.Add(BuildGet(tokenOption, verboseOption, noIndentOption));
        fileUploadsCmd.Subcommands.Add(BuildSendPart(tokenOption, verboseOption, noIndentOption));
        fileUploadsCmd.Subcommands.Add(BuildComplete(tokenOption, verboseOption, noIndentOption));
        fileUploadsCmd.Subcommands.Add(BuildUpload(tokenOption, verboseOption, noIndentOption));
        return fileUploadsCmd;
    }

    private static Command BuildCreate(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var filenameOption = new Option<string?>("--filename") { Description = "The original filename of the file being uploaded." };
        var contentTypeOption = new Option<string?>("--content-type") { Description = "The MIME type of the file (e.g. image/png)." };
        var sizeOption = new Option<long?>("--size") { Description = "The total size of the file in bytes." };
        var numberOfPartsOption = new Option<int?>("--number-of-parts") { Description = "The number of parts for a multi-part upload." };

        var cmd = new Command("create", "Initiate a new file upload session.")
        {
            filenameOption, contentTypeOption, sizeOption, numberOfPartsOption,
        };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);

                var filename = parseResult.GetValue(filenameOption);
                var contentType = parseResult.GetValue(contentTypeOption);
                var size = parseResult.GetValue(sizeOption);
                var numberOfParts = parseResult.GetValue(numberOfPartsOption);

                var request = (filename is not null || contentType is not null || size is not null || numberOfParts is not null)
                    ? new CreateFileUploadRequest
                    {
                        Filename = filename,
                        ContentType = contentType,
                        Size = size,
                        NumberOfParts = numberOfParts,
                    }
                    : null;

                var result = await client.FileUploads.Create(request, ct);
                JsonOutputHelper.Write<FileUpload>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildGet(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var fileUploadIdArg = new Argument<string>("file-upload-id") { Description = "The file upload identifier." };
        var cmd = new Command("get", "Retrieve the current status and metadata of a file upload.") { fileUploadIdArg };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var result = await client.FileUploads.Get(parseResult.GetValue(fileUploadIdArg)!, ct);
                JsonOutputHelper.Write<FileUpload>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildSendPart(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var fileUploadIdArg = new Argument<string>("file-upload-id") { Description = "The file upload identifier." };
        var fileOption = new Option<string>("--file") { Description = "Path to the file to upload.", Required = true };
        var contentTypeOption = new Option<string>("--content-type") { Description = "The MIME type of the file.", Required = true };
        var partNumberOption = new Option<int?>("--part-number") { Description = "The part number for multi-part uploads." };

        var cmd = new Command("send-part", "Upload a single part (or the entirety) of the file content.")
        {
            fileUploadIdArg, fileOption, contentTypeOption, partNumberOption,
        };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);

                var filePath = parseResult.GetValue(fileOption)!;
                if (!File.Exists(filePath))
                {
                    throw new InvalidOperationException($"File not found: {filePath}");
                }

                await using var stream = File.OpenRead(filePath);
                var result = await client.FileUploads.SendPart(
                    parseResult.GetValue(fileUploadIdArg)!,
                    stream,
                    parseResult.GetValue(contentTypeOption)!,
                    parseResult.GetValue(partNumberOption),
                    ct);
                JsonOutputHelper.Write<FileUpload>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildComplete(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var fileUploadIdArg = new Argument<string>("file-upload-id") { Description = "The file upload identifier." };
        var cmd = new Command("complete", "Mark a file upload as complete.") { fileUploadIdArg };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var result = await client.FileUploads.Complete(parseResult.GetValue(fileUploadIdArg)!, ct);
                JsonOutputHelper.Write<FileUpload>(result, !parseResult.GetValue(noIndentOption));
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static Command BuildUpload(
        Option<string?> tokenOption,
        Option<bool> verboseOption,
        Option<bool> noIndentOption)
    {
        var fileOption = new Option<string>("--file") { Description = "Path to the file to upload.", Required = true };
        var contentTypeOption = new Option<string?>("--content-type") { Description = "The MIME type of the file. Inferred from extension if not specified." };

        var cmd = new Command("upload", "Convenience: create + send-part + complete in one step.")
        {
            fileOption, contentTypeOption,
        };
        cmd.SetAction(async (parseResult, ct) =>
        {
            try
            {
                var verbose = parseResult.GetValue(verboseOption);
                var token = TokenResolver.Resolve(parseResult.GetValue(tokenOption), verbose);
                var client = NotionClientFactory.Create(token);
                var indent = !parseResult.GetValue(noIndentOption);

                var filePath = parseResult.GetValue(fileOption)!;
                if (!File.Exists(filePath))
                {
                    throw new InvalidOperationException($"File not found: {filePath}");
                }

                var filename = Path.GetFileName(filePath);
                var contentType = parseResult.GetValue(contentTypeOption) ?? InferContentType(filePath);
                var fileInfo = new FileInfo(filePath);

                // Step 1: Create upload session
                var createRequest = new CreateFileUploadRequest
                {
                    Filename = filename,
                    ContentType = contentType,
                    Size = fileInfo.Length,
                };
                var upload = await client.FileUploads.Create(createRequest, ct);

                // Step 2: Send file content
                await using var stream = File.OpenRead(filePath);
                upload = await client.FileUploads.SendPart(upload.Id, stream, contentType, cancellationToken: ct);

                // Step 3: Complete upload
                upload = await client.FileUploads.Complete(upload.Id, ct);

                JsonOutputHelper.Write<FileUpload>(upload, indent);
                return 0;
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleException(ex, parseResult.GetValue(verboseOption));
            }
        });
        return cmd;
    }

    private static string InferContentType(string filePath)
    {
        var ext = Path.GetExtension(filePath).ToLowerInvariant();
        return ext switch
        {
            ".png" => "image/png",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".svg" => "image/svg+xml",
            ".pdf" => "application/pdf",
            ".txt" => "text/plain",
            ".md" => "text/markdown",
            ".json" => "application/json",
            ".csv" => "text/csv",
            ".zip" => "application/zip",
            _ => "application/octet-stream",
        };
    }
}
