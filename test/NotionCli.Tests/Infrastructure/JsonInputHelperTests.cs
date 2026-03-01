// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionCli.Infrastructure;

namespace DamianH.NotionCli;

public sealed class JsonInputHelperTests
{
    [Fact]
    public void Read_ReturnsInlineJson_WhenArgIsPlainString()
    {
        var json = "{\"key\":\"value\"}";
        var result = JsonInputHelper.Read(json);
        result.ShouldBe(json);
    }

    [Fact]
    public void Read_ReadsFromFile_WhenArgStartsWithAt()
    {
        var json = "{\"foo\":\"bar\"}";
        var tempFile = Path.GetTempFileName();
        try
        {
            File.WriteAllText(tempFile, json);
            var result = JsonInputHelper.Read($"@{tempFile}");
            result.ShouldBe(json);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void Read_ThrowsInvalidOperationException_WhenFileNotFound()
    {
        var nonExistent = "@/nonexistent/path/file.json";
        Should.Throw<InvalidOperationException>(() => JsonInputHelper.Read(nonExistent));
    }

    [Fact]
    public void Read_ThrowsInvalidOperationException_WhenNoInputAvailable()
    {
        // When stdin is not redirected and no arg is provided, throws
        // Note: in test runner, Console.IsInputRedirected may be false
        if (!Console.IsInputRedirected)
        {
            Should.Throw<InvalidOperationException>(() => JsonInputHelper.Read(null));
        }
    }

    [Fact]
    public void ReadOptional_ReturnsInlineJson_WhenArgIsPlainString()
    {
        var json = "{\"hello\":\"world\"}";
        var result = JsonInputHelper.ReadOptional(json);
        result.ShouldBe(json);
    }

    [Fact]
    public void ReadOptional_ReadsFromFile_WhenArgStartsWithAt()
    {
        var json = "{\"hello\":\"world\"}";
        var tempFile = Path.GetTempFileName();
        try
        {
            File.WriteAllText(tempFile, json);
            var result = JsonInputHelper.ReadOptional($"@{tempFile}");
            result.ShouldBe(json);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void ReadOptional_ReturnsNull_WhenArgIsNullAndStdinNotRedirected()
    {
        if (!Console.IsInputRedirected)
        {
            var result = JsonInputHelper.ReadOptional(null);
            result.ShouldBeNull();
        }
    }

    [Fact]
    public void ReadOptional_ThrowsInvalidOperationException_WhenFileNotFound()
    {
        var nonExistent = "@/nonexistent/path/file.json";
        Should.Throw<InvalidOperationException>(() => JsonInputHelper.ReadOptional(nonExistent));
    }
}
