// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Net;

namespace DamianH.NotionClient;

public sealed class ErrorHandlingTests
{
    private static NotionClient BuildClient(HttpMessageHandler handler)
    {
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.notion.com/v1/"),
        };
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "test-token");
        return new NotionClient(httpClient);
    }

    private static StubHttpHandler Respond(HttpStatusCode code, string body) =>
        new(new HttpResponseMessage(code)
        {
            Content = new StringContent(body, System.Text.Encoding.UTF8, "application/json"),
        });

    [Theory]
    [InlineData("unauthorized", NotionErrorCode.Unauthorized, HttpStatusCode.Unauthorized)]
    [InlineData("restricted_resource", NotionErrorCode.RestrictedResource, HttpStatusCode.Forbidden)]
    [InlineData("object_not_found", NotionErrorCode.ObjectNotFound, HttpStatusCode.NotFound)]
    [InlineData("rate_limited", NotionErrorCode.RateLimited, HttpStatusCode.TooManyRequests)]
    [InlineData("invalid_json", NotionErrorCode.InvalidJson, HttpStatusCode.BadRequest)]
    [InlineData("invalid_request_url", NotionErrorCode.InvalidRequestUrl, HttpStatusCode.BadRequest)]
    [InlineData("invalid_request", NotionErrorCode.InvalidRequest, HttpStatusCode.BadRequest)]
    [InlineData("validation_error", NotionErrorCode.ValidationError, HttpStatusCode.UnprocessableEntity)]
    [InlineData("conflict_error", NotionErrorCode.ConflictError, HttpStatusCode.Conflict)]
    [InlineData("internal_server_error", NotionErrorCode.InternalServerError, HttpStatusCode.InternalServerError)]
    [InlineData("service_unavailable", NotionErrorCode.ServiceUnavailable, HttpStatusCode.ServiceUnavailable)]
    public async Task MapsKnownErrorCodesToEnum(string apiCode, NotionErrorCode expectedCode, HttpStatusCode statusCode)
    {
        var json = $$"""{"object":"error","status":{{(int)statusCode}},"code":"{{apiCode}}","message":"Something went wrong","request_id":"req-abc"}""";
        var client = BuildClient(Respond(statusCode, json));

        var ex = await Should.ThrowAsync<NotionApiException>(() =>
            client.Send<object>(HttpMethod.Get, "pages/abc"));

        ex.StatusCode.ShouldBe(statusCode);
        ex.ErrorCode.ShouldBe(expectedCode);
        ex.Message.ShouldBe("Something went wrong");
        ex.RequestId.ShouldBe("req-abc");
    }

    [Fact]
    public async Task UnknownErrorCodeProducesNullEnum()
    {
        const string json = """{"object":"error","status":400,"code":"unknown_future_code","message":"Oops","request_id":"req-xyz"}""";
        var client = BuildClient(Respond(HttpStatusCode.BadRequest, json));

        var ex = await Should.ThrowAsync<NotionApiException>(() =>
            client.Send<object>(HttpMethod.Get, "pages/abc"));

        ex.ErrorCode.ShouldBeNull();
        ex.RequestId.ShouldBe("req-xyz");
    }

    [Fact]
    public async Task NonJsonBodyProducesFallbackMessage()
    {
        var handler = new StubHttpHandler(new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent("Internal Server Error", System.Text.Encoding.UTF8, "text/plain"),
        });
        var client = BuildClient(handler);

        var ex = await Should.ThrowAsync<NotionApiException>(() =>
            client.Send<object>(HttpMethod.Get, "pages/abc"));

        ex.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
        ex.ErrorCode.ShouldBeNull();
        ex.Message.ShouldContain("500");
    }

    [Fact]
    public async Task SuccessResponseDoesNotThrow()
    {
        var handler = new StubHttpHandler(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("""{"object":"page","id":"abc"}""", System.Text.Encoding.UTF8, "application/json"),
        });
        var client = BuildClient(handler);

        var _ = await client.Send<System.Text.Json.JsonElement>(HttpMethod.Get, "pages/abc");
    }

    private sealed class StubHttpHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;
        public StubHttpHandler(HttpResponseMessage response) => _response = response;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => Task.FromResult(_response);
    }
}
