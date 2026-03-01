// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;
using DamianH.NotionClient.Models.Properties.Values;
using DamianH.NotionClient.Models.RichText;

namespace DamianH.NotionClient.Models;

public sealed class PropertyValueSerializationExtendedTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void RichTextPropertyValue_Deserializes()
    {
        var json = """{"id":"rt","type":"rich_text","rich_text":[{"type":"text","text":{"content":"Hello"},"plain_text":"Hello"}]}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var richText = prop.ShouldBeOfType<RichTextPropertyValue>();
        richText.RichText.ShouldHaveSingleItem();
        ((TextRichTextItem)richText.RichText[0]).Text.Content.ShouldBe("Hello");
    }

    [Fact]
    public void MultiSelectPropertyValue_Deserializes()
    {
        var json = """{"id":"ms","type":"multi_select","multi_select":[{"id":"a","name":"Tag1","color":"blue"},{"id":"b","name":"Tag2","color":"red"}]}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var multiSelect = prop.ShouldBeOfType<MultiSelectPropertyValue>();
        multiSelect.MultiSelect.Count.ShouldBe(2);
        multiSelect.MultiSelect[0].Name.ShouldBe("Tag1");
        multiSelect.MultiSelect[1].Name.ShouldBe("Tag2");
    }

    [Fact]
    public void StatusPropertyValue_Deserializes()
    {
        var json = """{"id":"st","type":"status","status":{"id":"s1","name":"In Progress","color":"yellow"}}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var status = prop.ShouldBeOfType<StatusPropertyValue>();
        status.Status.ShouldNotBeNull();
        status.Status.Name.ShouldBe("In Progress");
    }

    [Fact]
    public void PeoplePropertyValue_Deserializes()
    {
        var json = """{"id":"ppl","type":"people","people":[{"object":"user","id":"u1","type":"person","name":"Alice","person":{"email":"a@b.com"}}]}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var people = prop.ShouldBeOfType<PeoplePropertyValue>();
        people.People.ShouldHaveSingleItem();
        people.People[0].Id.ShouldBe("u1");
    }

    [Fact]
    public void FilesPropertyValue_Deserializes()
    {
        var json = """{"id":"f","type":"files","files":[{"type":"external","name":"doc.pdf","external":{"url":"https://example.com/doc.pdf"}}]}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var files = prop.ShouldBeOfType<FilesPropertyValue>();
        files.Files.ShouldHaveSingleItem();
        var externalFile = files.Files[0].ShouldBeOfType<ExternalFileReference>();
        externalFile.Name.ShouldBe("doc.pdf");
        externalFile.External.Url.ShouldBe("https://example.com/doc.pdf");
    }

    [Fact]
    public void UrlPropertyValue_Deserializes()
    {
        var json = """{"id":"u","type":"url","url":"https://example.com"}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var url = prop.ShouldBeOfType<UrlPropertyValue>();
        url.Url.ShouldBe("https://example.com");
    }

    [Fact]
    public void EmailPropertyValue_Deserializes()
    {
        var json = """{"id":"em","type":"email","email":"test@example.com"}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var email = prop.ShouldBeOfType<EmailPropertyValue>();
        email.Email.ShouldBe("test@example.com");
    }

    [Fact]
    public void PhoneNumberPropertyValue_Deserializes()
    {
        var json = """{"id":"ph","type":"phone_number","phone_number":"+1-555-0100"}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var phone = prop.ShouldBeOfType<PhoneNumberPropertyValue>();
        phone.PhoneNumber.ShouldBe("+1-555-0100");
    }

    [Fact]
    public void RelationPropertyValue_Deserializes()
    {
        var json = """{"id":"rel","type":"relation","relation":[{"id":"page-1"}],"has_more":false}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var relation = prop.ShouldBeOfType<RelationPropertyValue>();
        relation.Relation.ShouldHaveSingleItem();
        relation.Relation[0].Id.ShouldBe("page-1");
        relation.HasMore.ShouldBeFalse();
    }

    [Fact]
    public void FormulaPropertyValue_StringResult_Deserializes()
    {
        var json = """{"id":"fm","type":"formula","formula":{"type":"string","string":"hello"}}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var formula = prop.ShouldBeOfType<FormulaPropertyValue>();
        var stringResult = formula.Formula.ShouldBeOfType<StringFormulaResult>();
        stringResult.String.ShouldBe("hello");
    }

    [Fact]
    public void FormulaPropertyValue_NumberResult_Deserializes()
    {
        var json = """{"id":"fm2","type":"formula","formula":{"type":"number","number":42}}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var formula = prop.ShouldBeOfType<FormulaPropertyValue>();
        var numberResult = formula.Formula.ShouldBeOfType<NumberFormulaResult>();
        numberResult.Number.ShouldBe(42.0);
    }

    [Fact]
    public void FormulaPropertyValue_BooleanResult_Deserializes()
    {
        var json = """{"id":"fm3","type":"formula","formula":{"type":"boolean","boolean":true}}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var formula = prop.ShouldBeOfType<FormulaPropertyValue>();
        var booleanResult = formula.Formula.ShouldBeOfType<BooleanFormulaResult>();
        booleanResult.Boolean.ShouldBe(true);
    }

    [Fact]
    public void FormulaPropertyValue_DateResult_Deserializes()
    {
        var json = """{"id":"fm4","type":"formula","formula":{"type":"date","date":{"start":"2024-01-01","end":null,"time_zone":null}}}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var formula = prop.ShouldBeOfType<FormulaPropertyValue>();
        var dateResult = formula.Formula.ShouldBeOfType<DateFormulaResult>();
        dateResult.Date.ShouldNotBeNull();
        dateResult.Date.Start.ShouldBe("2024-01-01");
    }

    [Fact]
    public void RollupPropertyValue_NumberResult_Deserializes()
    {
        var json = """{"id":"ru","type":"rollup","rollup":{"type":"number","number":100,"function":"sum"}}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var rollup = prop.ShouldBeOfType<RollupPropertyValue>();
        var numberResult = rollup.Rollup.ShouldBeOfType<NumberRollupResult>();
        numberResult.Number.ShouldBe(100.0);
        numberResult.Function.ShouldBe("sum");
    }

    [Fact]
    public void RollupPropertyValue_DateResult_Deserializes()
    {
        var json = """{"id":"ru2","type":"rollup","rollup":{"type":"date","date":{"start":"2024-06-01","end":null,"time_zone":null},"function":"latest_date"}}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var rollup = prop.ShouldBeOfType<RollupPropertyValue>();
        var dateResult = rollup.Rollup.ShouldBeOfType<DateRollupResult>();
        dateResult.Date.ShouldNotBeNull();
        dateResult.Date.Start.ShouldBe("2024-06-01");
        dateResult.Function.ShouldBe("latest_date");
    }

    [Fact]
    public void RollupPropertyValue_ArrayResult_Deserializes()
    {
        var json = """{"id":"ru3","type":"rollup","rollup":{"type":"array","array":[{"type":"number","number":1},{"type":"number","number":2}],"function":"show_original"}}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var rollup = prop.ShouldBeOfType<RollupPropertyValue>();
        var arrayResult = rollup.Rollup.ShouldBeOfType<ArrayRollupResult>();
        arrayResult.Array.Count.ShouldBe(2);
        arrayResult.Function.ShouldBe("show_original");
        arrayResult.Array[0].ShouldBeOfType<NumberPropertyValue>();
    }

    [Fact]
    public void CreatedByPropertyValue_Deserializes()
    {
        var json = """{"id":"cb","type":"created_by","created_by":{"object":"user","id":"u1","type":"person","name":"Alice","person":{"email":"a@b.com"}}}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var createdBy = prop.ShouldBeOfType<CreatedByPropertyValue>();
        createdBy.CreatedBy.ShouldNotBeNull();
        createdBy.CreatedBy.Id.ShouldBe("u1");
    }

    [Fact]
    public void CreatedTimePropertyValue_Deserializes()
    {
        var json = """{"id":"ct","type":"created_time","created_time":"2024-01-01T12:00:00.000Z"}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var createdTime = prop.ShouldBeOfType<CreatedTimePropertyValue>();
        createdTime.CreatedTime.ShouldBe("2024-01-01T12:00:00.000Z");
    }

    [Fact]
    public void LastEditedByPropertyValue_Deserializes()
    {
        var json = """{"id":"leb","type":"last_edited_by","last_edited_by":{"object":"user","id":"u2","type":"person","name":"Bob","person":{"email":"b@b.com"}}}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var lastEditedBy = prop.ShouldBeOfType<LastEditedByPropertyValue>();
        lastEditedBy.LastEditedBy.ShouldNotBeNull();
        lastEditedBy.LastEditedBy.Id.ShouldBe("u2");
    }

    [Fact]
    public void LastEditedTimePropertyValue_Deserializes()
    {
        var json = """{"id":"let","type":"last_edited_time","last_edited_time":"2024-06-15T08:30:00.000Z"}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var lastEditedTime = prop.ShouldBeOfType<LastEditedTimePropertyValue>();
        lastEditedTime.LastEditedTime.ShouldBe("2024-06-15T08:30:00.000Z");
    }

    [Fact]
    public void ButtonPropertyValue_Deserializes()
    {
        var json = """{"id":"btn","type":"button","button":{}}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        prop.ShouldBeOfType<ButtonPropertyValue>();
    }

    [Fact]
    public void VerificationPropertyValue_Deserializes()
    {
        var json = """{"id":"ver","type":"verification","verification":{"state":"verified","verified_by":{"object":"user","id":"u1","type":"person","name":"Alice","person":{"email":"a@b.com"}},"date":{"start":"2024-01-01","end":null,"time_zone":null}}}""";

        var prop = JsonSerializer.Deserialize<PropertyValue>(json, JsonOptions);
        var verification = prop.ShouldBeOfType<VerificationPropertyValue>();
        verification.Verification.ShouldNotBeNull();
        verification.Verification.State.ShouldBe("verified");
        verification.Verification.VerifiedBy.ShouldNotBeNull();
        verification.Verification.VerifiedBy.Id.ShouldBe("u1");
        verification.Verification.Date.ShouldNotBeNull();
        verification.Verification.Date.Start.ShouldBe("2024-01-01");
    }
}
