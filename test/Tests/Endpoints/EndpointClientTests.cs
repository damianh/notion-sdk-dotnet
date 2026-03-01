// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Net;
using System.Net.Http.Headers;
using System.Text;
using DamianH.NotionClient.Models.Blocks;
using DamianH.NotionClient.Models.Common;
using DamianH.NotionClient.Models.Pagination;
using DamianH.NotionClient.Models.Requests;

namespace DamianH.NotionClient.Endpoints;

public sealed class EndpointClientTests
{
    private static (NotionClient client, CapturingHandler handler) Build(
        string responseJson,
        HttpStatusCode status = HttpStatusCode.OK)
    {
        var handler = new CapturingHandler(responseJson, status);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.notion.com/v1/"),
        };
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "secret_test");
        return (new NotionClient(httpClient), handler);
    }

    private static string PageJson(string id = "page-id") =>
        $$"""{"object":"page","id":"{{id}}"}""";

    private static string BlockJson(string id = "block-id") =>
        $$$"""{"object":"block","id":"{{{id}}}","type":"paragraph","paragraph":{"rich_text":[],"color":"default"}}""";

    private static string DatabaseJson(string id = "db-id") =>
        $$$"""{"object":"database","id":"{{{id}}}","title":[],"properties":{}}""";

    private static string UserJson(string id = "user-id") =>
        $$"""{"object":"user","id":"{{id}}","type":"person","name":"Test"}""";

    private static string CommentJson(string id = "comment-id") =>
        $$"""{"object":"comment","id":"{{id}}","discussion_id":"disc-id","rich_text":[]}""";

    private static string PaginatedJson(string itemJson) =>
        $$"""{"object":"list","results":[{{itemJson}}],"next_cursor":null,"has_more":false}""";

    [Fact]
    public async Task Blocks_Get_SendsGetToCorrectUrl()
    {
        var (client, handler) = Build(BlockJson());
        var result = await client.Blocks.Get("block-123");

        handler.LastMethod.ShouldBe(HttpMethod.Get);
        handler.LastUrl.ShouldContain("blocks/block-123");
        result.Id.ShouldBe("block-id");
    }

    [Fact]
    public async Task Blocks_Delete_SendsDeleteToCorrectUrl()
    {
        var (client, handler) = Build(BlockJson());
        await client.Blocks.Delete("block-456");

        handler.LastMethod.ShouldBe(HttpMethod.Delete);
        handler.LastUrl.ShouldContain("blocks/block-456");
    }

    [Fact]
    public async Task Blocks_ListChildren_SendsGetToChildrenUrl()
    {
        var (client, handler) = Build(PaginatedJson(BlockJson()));
        var result = await client.Blocks.ListChildren("block-parent");

        handler.LastMethod.ShouldBe(HttpMethod.Get);
        handler.LastUrl.ShouldContain("blocks/block-parent/children");
        result.Results.ShouldHaveSingleItem();
    }

    [Fact]
    public async Task Blocks_AppendChildren_SendsPatchToChildrenUrl()
    {
        var (client, handler) = Build(PaginatedJson(BlockJson()));
        await client.Blocks.AppendChildren("block-parent",
            new AppendBlockChildrenRequest { Children = [] });

        handler.LastMethod.ShouldBe(HttpMethod.Patch);
        handler.LastUrl.ShouldContain("blocks/block-parent/children");
    }

    [Fact]
    public async Task Blocks_Update_SendsPatchToCorrectUrl()
    {
        var (client, handler) = Build(BlockJson());
        var block = new ParagraphBlock { Paragraph = new RichTextWithColorAndChildren() };
        await client.Blocks.Update("block-789", block);

        handler.LastMethod.ShouldBe(HttpMethod.Patch);
        handler.LastUrl.ShouldContain("blocks/block-789");
    }

    [Fact]
    public async Task Databases_Get_SendsGetToCorrectUrl()
    {
        var (client, handler) = Build(DatabaseJson());
        var result = await client.Databases.Get("db-123");

        handler.LastMethod.ShouldBe(HttpMethod.Get);
        handler.LastUrl.ShouldContain("databases/db-123");
        result.Id.ShouldBe("db-id");
    }

    [Fact]
    public async Task Databases_Create_SendsPostToDatabasesUrl()
    {
        var (client, handler) = Build(DatabaseJson());
        await client.Databases.Create(new CreateDatabaseRequest
        {
            Parent = new PageParent { PageId = "parent-page-id" },
        });

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldEndWith("databases");
    }

    [Fact]
    public async Task Databases_Update_SendsPatchToCorrectUrl()
    {
        var (client, handler) = Build(DatabaseJson());
        await client.Databases.Update("db-456", new UpdateDatabaseRequest());

        handler.LastMethod.ShouldBe(HttpMethod.Patch);
        handler.LastUrl.ShouldContain("databases/db-456");
    }

    [Fact]
    public async Task Databases_Query_SendsPostToQueryUrl()
    {
        var (client, handler) = Build(PaginatedJson(PageJson()));
        var result = await client.Databases.Query("db-789");

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldContain("databases/db-789/query");
        result.Results.ShouldHaveSingleItem();
    }

    [Fact]
    public async Task Pages_Get_SendsGetToCorrectUrl()
    {
        var (client, handler) = Build(PageJson("pg-abc"));
        var result = await client.Pages.Get("pg-abc");

        handler.LastMethod.ShouldBe(HttpMethod.Get);
        handler.LastUrl.ShouldContain("pages/pg-abc");
        result.Id.ShouldBe("pg-abc");
    }

    [Fact]
    public async Task Pages_Create_SendsPostToPagesUrl()
    {
        var (client, handler) = Build(PageJson());
        await client.Pages.Create(new CreatePageRequest
        {
            Parent = new PageParent { PageId = "parent-id" },
        });

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldEndWith("pages");
    }

    [Fact]
    public async Task Pages_Update_SendsPatchToCorrectUrl()
    {
        var (client, handler) = Build(PageJson());
        await client.Pages.Update("pg-xyz", new UpdatePageRequest());

        handler.LastMethod.ShouldBe(HttpMethod.Patch);
        handler.LastUrl.ShouldContain("pages/pg-xyz");
    }

    [Fact]
    public async Task Pages_GetProperty_SendsGetToPropertyUrl()
    {
        var json = """{"type":"title","title":[]}""";
        var (client, handler) = Build(json);
        await client.Pages.GetProperty("pg-abc", "prop-123");

        handler.LastMethod.ShouldBe(HttpMethod.Get);
        handler.LastUrl.ShouldContain("pages/pg-abc/properties/prop-123");
    }

    [Fact]
    public async Task Users_GetSelf_SendsGetToUsersMeUrl()
    {
        var (client, handler) = Build(UserJson());
        await client.Users.GetSelf();

        handler.LastMethod.ShouldBe(HttpMethod.Get);
        handler.LastUrl.ShouldEndWith("users/me");
    }

    [Fact]
    public async Task Users_Get_SendsGetToCorrectUrl()
    {
        var (client, handler) = Build(UserJson("u-123"));
        var result = await client.Users.Get("u-123");

        handler.LastMethod.ShouldBe(HttpMethod.Get);
        handler.LastUrl.ShouldContain("users/u-123");
        result.Id.ShouldBe("u-123");
    }

    [Fact]
    public async Task Users_List_SendsGetToUsersUrl()
    {
        var (client, handler) = Build(PaginatedJson(UserJson()));
        var result = await client.Users.List();

        handler.LastMethod.ShouldBe(HttpMethod.Get);
        handler.LastUrl.ShouldEndWith("users");
        result.Results.ShouldHaveSingleItem();
    }

    [Fact]
    public async Task Comments_Create_SendsPostToCommentsUrl()
    {
        var (client, handler) = Build(CommentJson());
        await client.Comments.Create(new CreateCommentRequest
        {
            Parent = new BlockParent { BlockId = "block-id" },
            RichText = [],
        });

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldEndWith("comments");
    }

    [Fact]
    public async Task Comments_List_SendsGetWithBlockIdQuery()
    {
        var (client, handler) = Build(PaginatedJson(CommentJson()));
        await client.Comments.List("blk-id");

        handler.LastMethod.ShouldBe(HttpMethod.Get);
        handler.LastUrl.ShouldContain("block_id=blk-id");
    }

    [Fact]
    public async Task Search_Search_SendsPostToSearchUrl()
    {
        var (client, handler) = Build(PaginatedJson(PageJson()));
        var result = await client.Search.Search(new SearchRequest { Query = "hello" });

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldEndWith("search");
        (handler.LastRequestBody ?? "").ShouldContain("\"hello\"");
    }

    [Fact]
    public async Task Client_AttachesBearerAuthHeader()
    {
        var (client, handler) = Build(PageJson());
        await client.Pages.Get("pg-abc");

        handler.LastAuthorization.ShouldNotBeNull();
        handler.LastAuthorization!.Scheme.ShouldBe("Bearer");
        handler.LastAuthorization.Parameter.ShouldBe("secret_test");
    }

    [Fact]
    public async Task OAuth_ExchangeToken_UsesBasicAuth()
    {
        var json = """{"access_token":"tok","token_type":"bearer","bot_id":"bot-1","workspace_id":"ws-1"}""";
        var (client, handler) = Build(json);
        await client.OAuth.ExchangeToken("cid", "csecret",
            new ExchangeTokenRequest { Code = "auth-code", GrantType = "authorization_code", RedirectUri = "https://example.com" });

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldContain("oauth/token");
        handler.LastAuthorization!.Scheme.ShouldBe("Basic");
        var expected = Convert.ToBase64String(Encoding.UTF8.GetBytes("cid:csecret"));
        handler.LastAuthorization.Parameter.ShouldBe(expected);
    }

    [Fact]
    public async Task FileUploads_Get_SendsGetToCorrectUrl()
    {
        var json = """{"id":"fu-123","status":"pending"}""";
        var (client, handler) = Build(json);
        var result = await client.FileUploads.Get("fu-123");

        handler.LastMethod.ShouldBe(HttpMethod.Get);
        handler.LastUrl.ShouldContain("file_uploads/fu-123");
        result.Id.ShouldBe("fu-123");
    }

    [Fact]
    public async Task FileUploads_Create_SendsPostToFileUploadsUrl()
    {
        var json = """{"id":"fu-new","status":"pending"}""";
        var (client, handler) = Build(json);
        await client.FileUploads.Create();

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldEndWith("file_uploads");
    }

    [Fact]
    public async Task FileUploads_Complete_SendsPostToCompleteUrl()
    {
        var json = """{"id":"fu-done","status":"uploaded"}""";
        var (client, handler) = Build(json);
        await client.FileUploads.Complete("fu-done");

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldContain("file_uploads/fu-done/complete");
    }

    [Fact]
    public async Task FileUploads_SendPart_SendsPostMultipartToSendUrl()
    {
        var json = """{"id":"fu-part","status":"pending"}""";
        var (client, handler) = Build(json);
        using var stream = new MemoryStream("file-bytes"u8.ToArray());
        await client.FileUploads.SendPart("fu-part", stream, "application/octet-stream", partNumber: 1);

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldContain("file_uploads/fu-part/send");
        handler.LastUrl.ShouldContain("part_number=1");
    }

    [Fact]
    public async Task DataSources_Get_SendsGetToCorrectUrl()
    {
        var (client, handler) = Build(DatabaseJson("ds-id"));
        var result = await client.DataSources.Get("ds-id");

        handler.LastMethod.ShouldBe(HttpMethod.Get);
        handler.LastUrl.ShouldContain("databases/ds-id");
        result.Id.ShouldBe("ds-id");
    }

    [Fact]
    public async Task DataSources_Create_SendsPostToDatabasesUrl()
    {
        var (client, handler) = Build(DatabaseJson("ds-new"));
        await client.DataSources.Create(new CreateDatabaseRequest
        {
            Parent = new PageParent { PageId = "parent-id" },
        });

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldEndWith("databases");
    }

    [Fact]
    public async Task DataSources_Update_SendsPatchToCorrectUrl()
    {
        var (client, handler) = Build(DatabaseJson("ds-upd"));
        await client.DataSources.Update("ds-upd", new UpdateDatabaseRequest());

        handler.LastMethod.ShouldBe(HttpMethod.Patch);
        handler.LastUrl.ShouldContain("databases/ds-upd");
    }

    [Fact]
    public async Task DataSources_Query_SendsPostToQueryUrl()
    {
        var (client, handler) = Build(PaginatedJson(PageJson()));
        var result = await client.DataSources.Query("ds-q");

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldContain("databases/ds-q/query");
        result.Results.ShouldHaveSingleItem();
    }

    [Fact]
    public async Task OAuth_Revoke_SendsPostWithBasicAuth()
    {
        var (client, handler) = Build("null");
        await client.OAuth.Revoke("cid", "csecret",
            new RevokeTokenRequest { Token = "tok-to-revoke" });

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldContain("oauth/revoke");
        handler.LastAuthorization!.Scheme.ShouldBe("Basic");
    }

    [Fact]
    public async Task OAuth_Introspect_SendsPostWithBasicAuth()
    {
        var json = """{"active":true,"bot_id":"bot-1"}""";
        var (client, handler) = Build(json);
        var result = await client.OAuth.Introspect("cid", "csecret",
            new IntrospectTokenRequest { Token = "tok-to-check" });

        handler.LastMethod.ShouldBe(HttpMethod.Post);
        handler.LastUrl.ShouldContain("oauth/introspect");
        handler.LastAuthorization!.Scheme.ShouldBe("Basic");
        result.Active.ShouldBeTrue();
    }

    [Fact]
    public async Task Users_List_WithPagination_AppendsQueryParams()
    {
        var (client, handler) = Build(PaginatedJson(UserJson()));
        await client.Users.List(new PaginationParameters { StartCursor = "cursor-abc", PageSize = 10 });

        handler.LastUrl.ShouldContain("start_cursor=cursor-abc");
        handler.LastUrl.ShouldContain("page_size=10");
    }

    private sealed class CapturingHandler : HttpMessageHandler
    {
        private readonly string _responseJson;
        private readonly HttpStatusCode _status;

        public HttpMethod? LastMethod { get; private set; }
        public string LastUrl { get; private set; } = string.Empty;
        public string? LastRequestBody { get; private set; }
        public AuthenticationHeaderValue? LastAuthorization { get; private set; }

        public CapturingHandler(string responseJson, HttpStatusCode status)
        {
            _responseJson = responseJson;
            _status = status;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            LastMethod = request.Method;
            LastUrl = request.RequestUri?.ToString() ?? string.Empty;
            LastAuthorization = request.Headers.Authorization;
            LastRequestBody = request.Content is not null
                ? await request.Content.ReadAsStringAsync(cancellationToken)
                : null;

            return new HttpResponseMessage(_status)
            {
                Content = new StringContent(_responseJson, Encoding.UTF8, "application/json"),
            };
        }
    }
}
