using System.Text.Json;
using System.Text.Json.Serialization;

var opts = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
};

// Test 1: type field FIRST
Console.WriteLine("=== type field first ===");
try {
    var j = """{"type":"b","object":"block","value":"hello"}""";
    var r = JsonSerializer.Deserialize<BaseX>(j, opts);
    Console.WriteLine($"OK: {r?.GetType().Name}");
} catch (Exception ex) { Console.WriteLine($"FAIL: {ex.Message.Split('\n')[0]}"); }

// Test 2: type field NOT first (like Notion API)
Console.WriteLine("=== type field not first ===");
try {
    var j = """{"object":"block","id":"x","type":"b","value":"hello"}""";
    var r = JsonSerializer.Deserialize<BaseX>(j, opts);
    Console.WriteLine($"OK: {r?.GetType().Name}");
} catch (Exception ex) { Console.WriteLine($"FAIL: {ex.Message.Split('\n')[0]}"); }

// Test 3: with AllowOutOfOrderMetadataProperties
Console.WriteLine("=== with AllowOutOfOrderMetadataProperties ===");
var opts3 = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    AllowOutOfOrderMetadataProperties = true,
};
try {
    var j = """{"object":"block","id":"x","type":"b","value":"hello"}""";
    var r = JsonSerializer.Deserialize<BaseX>(j, opts3);
    Console.WriteLine($"OK: {r?.GetType().Name}");
} catch (Exception ex) { Console.WriteLine($"FAIL: {ex.Message.Split('\n')[0]}"); }

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(DerivedB), "b")]
abstract class BaseX
{
    public BaseX() { }
    [JsonIgnore] public virtual string Type => string.Empty;
    [JsonPropertyName("object")] public string Object { get; init; } = null!;
    [JsonPropertyName("id")] public string Id { get; init; } = null!;
}
sealed class DerivedB : BaseX
{
    [JsonIgnore] public override string Type => "b";
    [JsonPropertyName("value")] public string Value { get; init; } = null!;
}
