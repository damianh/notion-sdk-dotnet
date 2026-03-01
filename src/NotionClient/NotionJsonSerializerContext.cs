// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;
using DamianH.NotionClient.Models;
using DamianH.NotionClient.Models.Blocks;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Properties.Schema;
using DamianH.NotionClient.Models.Properties.Values;
using DamianH.NotionClient.Models.Requests;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    AllowOutOfOrderMetadataProperties = true)]
// --- Response root types ---
[JsonSerializable(typeof(Page))]
[JsonSerializable(typeof(Database))]
[JsonSerializable(typeof(DataSource))]
[JsonSerializable(typeof(FileUpload))]
[JsonSerializable(typeof(Comment))]
// --- Polymorphic base types (response) ---
[JsonSerializable(typeof(Block))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(PropertyValue))]
[JsonSerializable(typeof(PropertySchema))]
[JsonSerializable(typeof(SearchResult))]
// --- Polymorphic inner types that need explicit registration ---
[JsonSerializable(typeof(TemplateMentionContent))]
// --- Paginated response types ---
[JsonSerializable(typeof(PaginatedList<Block>))]
[JsonSerializable(typeof(PaginatedList<Page>))]
[JsonSerializable(typeof(PaginatedList<User>))]
[JsonSerializable(typeof(PaginatedList<Comment>))]
[JsonSerializable(typeof(PaginatedList<SearchResult>))]
// --- Request body types ---
[JsonSerializable(typeof(CreatePageRequest))]
[JsonSerializable(typeof(UpdatePageRequest))]
[JsonSerializable(typeof(CreateDatabaseRequest))]
[JsonSerializable(typeof(UpdateDatabaseRequest))]
[JsonSerializable(typeof(QueryDatabaseRequest))]
[JsonSerializable(typeof(AppendBlockChildrenRequest))]
[JsonSerializable(typeof(UpdateBlockRequest))]
[JsonSerializable(typeof(CreateCommentRequest))]
[JsonSerializable(typeof(SearchRequest))]
[JsonSerializable(typeof(CreateFileUploadRequest))]
[JsonSerializable(typeof(ExchangeTokenRequest))]
[JsonSerializable(typeof(RevokeTokenRequest))]
[JsonSerializable(typeof(IntrospectTokenRequest))]
// --- OAuth response types ---
[JsonSerializable(typeof(ExchangeTokenResponse))]
[JsonSerializable(typeof(IntrospectTokenResponse))]
// --- Primitive types used by generic endpoints ---
[JsonSerializable(typeof(System.Text.Json.JsonElement))]
internal sealed partial class NotionJsonSerializerContext : JsonSerializerContext;
