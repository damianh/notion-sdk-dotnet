// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using DamianH.NotionCli.Auth;

namespace DamianH.NotionCli;

public sealed class TokenResolverTests
{
    [Fact]
    public void Resolve_ReturnsTokenOption_WhenProvided()
    {
        var result = TokenResolver.Resolve("explicit-token");
        result.ShouldBe("explicit-token");
    }

    [Fact]
    public void Resolve_ReturnsEnvVar_WhenTokenOptionIsNull()
    {
        var key = "NOTION_TOKEN";
        var original = Environment.GetEnvironmentVariable(key);
        try
        {
            Environment.SetEnvironmentVariable(key, "env-token");
            var result = TokenResolver.Resolve(null);
            result.ShouldBe("env-token");
        }
        finally
        {
            Environment.SetEnvironmentVariable(key, original);
        }
    }

    [Fact]
    public void Resolve_ReturnsConfigFileToken_WhenEnvVarIsAbsent()
    {
        var key = "NOTION_TOKEN";
        var original = Environment.GetEnvironmentVariable(key);
        var configPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".notion",
            "config.json");
        var configDir = Path.GetDirectoryName(configPath)!;
        var configExists = File.Exists(configPath);
        var configBackup = configExists ? File.ReadAllText(configPath) : null;

        try
        {
            Environment.SetEnvironmentVariable(key, null);
            Directory.CreateDirectory(configDir);
            File.WriteAllText(configPath, "{\"token\":\"config-token\"}");
            var result = TokenResolver.Resolve(null);
            result.ShouldBe("config-token");
        }
        finally
        {
            Environment.SetEnvironmentVariable(key, original);
            if (configBackup is not null)
            {
                File.WriteAllText(configPath, configBackup);
            }
            else if (File.Exists(configPath))
            {
                File.Delete(configPath);
            }
        }
    }

    [Fact]
    public void Resolve_ThrowsInvalidOperationException_WhenNoTokenAvailable()
    {
        var key = "NOTION_TOKEN";
        var original = Environment.GetEnvironmentVariable(key);
        var configPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".notion",
            "config.json");
        var configExists = File.Exists(configPath);
        var configBackup = configExists ? File.ReadAllText(configPath) : null;

        try
        {
            Environment.SetEnvironmentVariable(key, null);
            if (File.Exists(configPath))
            {
                File.Delete(configPath);
            }

            Should.Throw<InvalidOperationException>(() => TokenResolver.Resolve(null));
        }
        finally
        {
            Environment.SetEnvironmentVariable(key, original);
            if (configBackup is not null)
            {
                File.WriteAllText(configPath, configBackup);
            }
        }
    }

    [Fact]
    public void Resolve_TokenOptionTakesPrecedenceOverEnvVar()
    {
        var key = "NOTION_TOKEN";
        var original = Environment.GetEnvironmentVariable(key);
        try
        {
            Environment.SetEnvironmentVariable(key, "env-token");
            var result = TokenResolver.Resolve("explicit-token");
            result.ShouldBe("explicit-token");
        }
        finally
        {
            Environment.SetEnvironmentVariable(key, original);
        }
    }
}
