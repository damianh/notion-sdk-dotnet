// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionClient.Helpers;

namespace DamianH.NotionClient;

public sealed class IdHelperTests
{
    private const string ExpectedId = "9a2e7a04-9b90-4f67-8f5b-12d4ad3cc94f";

    [Fact]
    public void ExtractNotionId_ParsesHyphenatedUuid()
    {
        var result = IdHelpers.ExtractNotionId("9a2e7a04-9b90-4f67-8f5b-12d4ad3cc94f");
        result.ShouldBe(ExpectedId);
    }

    [Fact]
    public void ExtractNotionId_ParsesUnhyphenatedUuid()
    {
        var result = IdHelpers.ExtractNotionId("9a2e7a049b904f678f5b12d4ad3cc94f");
        result.ShouldBe(ExpectedId);
    }

    [Fact]
    public void ExtractNotionId_ParsesPageUrl()
    {
        var result = IdHelpers.ExtractNotionId(
            "https://www.notion.so/workspace/My-Page-9a2e7a049b904f678f5b12d4ad3cc94f");
        result.ShouldBe(ExpectedId);
    }

    [Fact]
    public void ExtractNotionId_ParsesShortUrl()
    {
        var result = IdHelpers.ExtractNotionId(
            "https://notion.so/9a2e7a049b904f678f5b12d4ad3cc94f");
        result.ShouldBe(ExpectedId);
    }

    [Fact]
    public void ExtractNotionId_ParsesUrlWithHyphenatedId()
    {
        var result = IdHelpers.ExtractNotionId(
            "https://notion.so/workspace/9a2e7a04-9b90-4f67-8f5b-12d4ad3cc94f");
        result.ShouldBe(ExpectedId);
    }

    [Fact]
    public void ExtractNotionId_NormalizesToLowercase()
    {
        var result = IdHelpers.ExtractNotionId("9A2E7A04-9B90-4F67-8F5B-12D4AD3CC94F");
        result.ShouldBe(ExpectedId);
    }

    [Fact]
    public void ExtractNotionId_ThrowsForInvalidInput() =>
        Should.Throw<ArgumentException>(() => IdHelpers.ExtractNotionId("not-a-uuid"));

    [Fact]
    public void ExtractNotionId_ThrowsForEmptyString() =>
        Should.Throw<ArgumentException>(() => IdHelpers.ExtractNotionId(""));

    [Fact]
    public void ExtractNotionId_ThrowsForWhitespace() =>
        Should.Throw<ArgumentException>(() => IdHelpers.ExtractNotionId("   "));

    [Fact]
    public void ExtractPageId_ReturnsSameAsExtractNotionId()
    {
        var input = "https://notion.so/workspace/My-Page-9a2e7a049b904f678f5b12d4ad3cc94f";
        IdHelpers.ExtractPageId(input).ShouldBe(IdHelpers.ExtractNotionId(input));
    }

    [Fact]
    public void ExtractDatabaseId_ReturnsSameAsExtractNotionId()
    {
        var input = "9a2e7a049b904f678f5b12d4ad3cc94f";
        IdHelpers.ExtractDatabaseId(input).ShouldBe(IdHelpers.ExtractNotionId(input));
    }

    [Fact]
    public void ExtractBlockId_ReturnsSameAsExtractNotionId()
    {
        var input = "9a2e7a04-9b90-4f67-8f5b-12d4ad3cc94f";
        IdHelpers.ExtractBlockId(input).ShouldBe(IdHelpers.ExtractNotionId(input));
    }

    [Fact]
    public void TryExtractNotionId_ReturnsTrueForValidUuid()
    {
        var success = IdHelpers.TryExtractNotionId("9a2e7a049b904f678f5b12d4ad3cc94f", out var id);
        success.ShouldBeTrue();
        id.ShouldBe(ExpectedId);
    }

    [Fact]
    public void TryExtractNotionId_ReturnsFalseForInvalidInput()
    {
        var success = IdHelpers.TryExtractNotionId("not-a-uuid", out var id);
        success.ShouldBeFalse();
        id.ShouldBeNull();
    }

    [Fact]
    public void TryExtractNotionId_ReturnsFalseForEmptyString()
    {
        var success = IdHelpers.TryExtractNotionId("", out var id);
        success.ShouldBeFalse();
        id.ShouldBeNull();
    }

    [Fact]
    public void TryExtractNotionId_ReturnsFalseForWhitespace()
    {
        var success = IdHelpers.TryExtractNotionId("   ", out var id);
        success.ShouldBeFalse();
        id.ShouldBeNull();
    }

    [Fact]
    public void TryExtractNotionId_ParsesUrlSuccessfully()
    {
        var success = IdHelpers.TryExtractNotionId(
            "https://www.notion.so/workspace/My-Page-9a2e7a049b904f678f5b12d4ad3cc94f",
            out var id);
        success.ShouldBeTrue();
        id.ShouldBe(ExpectedId);
    }
}
