// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json;
using DamianH.NotionClient.Models.Properties.Schema;

namespace DamianH.NotionClient.Models;

public sealed class PropertySchemaSerializationExtendedTests
{
    private static readonly JsonSerializerOptions JsonOptions = NotionJsonSerializerOptions.Default;

    [Fact]
    public void TitlePropertySchema_Deserializes()
    {
        var json = """{"id":"t","name":"Name","type":"title","title":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var title = schema.ShouldBeOfType<TitlePropertySchema>();
        title.Name.ShouldBe("Name");
    }

    [Fact]
    public void RichTextPropertySchema_Deserializes()
    {
        var json = """{"id":"rt","name":"Notes","type":"rich_text","rich_text":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var richText = schema.ShouldBeOfType<RichTextPropertySchema>();
        richText.Name.ShouldBe("Notes");
    }

    [Fact]
    public void MultiSelectPropertySchema_DeserializesOptions()
    {
        var json = """{"id":"ms","name":"Tags","type":"multi_select","multi_select":{"options":[{"id":"a","name":"Tag1","color":"blue"}]}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var multiSelect = schema.ShouldBeOfType<MultiSelectPropertySchema>();
        multiSelect.MultiSelect.ShouldNotBeNull();
        multiSelect.MultiSelect.Options.ShouldHaveSingleItem();
        multiSelect.MultiSelect.Options[0].Name.ShouldBe("Tag1");
    }

    [Fact]
    public void StatusPropertySchema_DeserializesOptionsAndGroups()
    {
        var json = """{"id":"st","name":"Status","type":"status","status":{"options":[{"id":"s1","name":"To Do","color":"default"}],"groups":[{"id":"g1","name":"To do","color":"gray","option_ids":["s1"]}]}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var status = schema.ShouldBeOfType<StatusPropertySchema>();
        status.Status.ShouldNotBeNull();
        status.Status.Options.ShouldHaveSingleItem();
        status.Status.Options[0].Name.ShouldBe("To Do");
        status.Status.Groups.ShouldHaveSingleItem();
        status.Status.Groups[0].Name.ShouldBe("To do");
        status.Status.Groups[0].OptionIds.ShouldHaveSingleItem();
        status.Status.Groups[0].OptionIds[0].ShouldBe("s1");
    }

    [Fact]
    public void DatePropertySchema_Deserializes()
    {
        var json = """{"id":"d","name":"Due","type":"date","date":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var date = schema.ShouldBeOfType<DatePropertySchema>();
        date.Name.ShouldBe("Due");
    }

    [Fact]
    public void PeoplePropertySchema_Deserializes()
    {
        var json = """{"id":"p","name":"Assignee","type":"people","people":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var people = schema.ShouldBeOfType<PeoplePropertySchema>();
        people.Name.ShouldBe("Assignee");
    }

    [Fact]
    public void FilesPropertySchema_Deserializes()
    {
        var json = """{"id":"f","name":"Attachments","type":"files","files":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var files = schema.ShouldBeOfType<FilesPropertySchema>();
        files.Name.ShouldBe("Attachments");
    }

    [Fact]
    public void CheckboxPropertySchema_Deserializes()
    {
        var json = """{"id":"cb","name":"Done","type":"checkbox","checkbox":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var checkbox = schema.ShouldBeOfType<CheckboxPropertySchema>();
        checkbox.Name.ShouldBe("Done");
    }

    [Fact]
    public void UrlPropertySchema_Deserializes()
    {
        var json = """{"id":"u","name":"Link","type":"url","url":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var url = schema.ShouldBeOfType<UrlPropertySchema>();
        url.Name.ShouldBe("Link");
    }

    [Fact]
    public void EmailPropertySchema_Deserializes()
    {
        var json = """{"id":"em","name":"Email","type":"email","email":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var email = schema.ShouldBeOfType<EmailPropertySchema>();
        email.Name.ShouldBe("Email");
    }

    [Fact]
    public void PhoneNumberPropertySchema_Deserializes()
    {
        var json = """{"id":"ph","name":"Phone","type":"phone_number","phone_number":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var phone = schema.ShouldBeOfType<PhoneNumberPropertySchema>();
        phone.Name.ShouldBe("Phone");
    }

    [Fact]
    public void RelationPropertySchema_SingleProperty_Deserializes()
    {
        var json = """{"id":"rel","name":"Related","type":"relation","relation":{"database_id":"db-1","type":"single_property","single_property":{}}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var relation = schema.ShouldBeOfType<RelationPropertySchema>();
        relation.Name.ShouldBe("Related");
        relation.Relation.ShouldNotBeNull();
        var singleProp = relation.Relation.ShouldBeOfType<SinglePropertyRelationConfig>();
        singleProp.DatabaseId.ShouldBe("db-1");
    }

    [Fact]
    public void RelationPropertySchema_DualProperty_Deserializes()
    {
        var json = """{"id":"rel2","name":"Related2","type":"relation","relation":{"database_id":"db-2","type":"dual_property","dual_property":{"synced_property_id":"prop-x","synced_property_name":"Reverse"}}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var relation = schema.ShouldBeOfType<RelationPropertySchema>();
        relation.Name.ShouldBe("Related2");
        relation.Relation.ShouldNotBeNull();
        var dualProp = relation.Relation.ShouldBeOfType<DualPropertyRelationConfig>();
        dualProp.DatabaseId.ShouldBe("db-2");
        dualProp.DualProperty.ShouldNotBeNull();
        dualProp.DualProperty.SyncedPropertyId.ShouldBe("prop-x");
        dualProp.DualProperty.SyncedPropertyName.ShouldBe("Reverse");
    }

    [Fact]
    public void RollupPropertySchema_Deserializes()
    {
        var json = """{"id":"ru","name":"Total","type":"rollup","rollup":{"relation_property_name":"Tasks","relation_property_id":"rel1","rollup_property_name":"Points","rollup_property_id":"pts","function":"sum"}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var rollup = schema.ShouldBeOfType<RollupPropertySchema>();
        rollup.Rollup.ShouldNotBeNull();
        rollup.Rollup.RelationPropertyName.ShouldBe("Tasks");
        rollup.Rollup.Function.ShouldBe("sum");
    }

    [Fact]
    public void CreatedByPropertySchema_Deserializes()
    {
        var json = """{"id":"cb2","name":"Created By","type":"created_by","created_by":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var createdBy = schema.ShouldBeOfType<CreatedByPropertySchema>();
        createdBy.Name.ShouldBe("Created By");
    }

    [Fact]
    public void CreatedTimePropertySchema_Deserializes()
    {
        var json = """{"id":"ct","name":"Created","type":"created_time","created_time":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var createdTime = schema.ShouldBeOfType<CreatedTimePropertySchema>();
        createdTime.Name.ShouldBe("Created");
    }

    [Fact]
    public void LastEditedByPropertySchema_Deserializes()
    {
        var json = """{"id":"leb","name":"Edited By","type":"last_edited_by","last_edited_by":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var lastEditedBy = schema.ShouldBeOfType<LastEditedByPropertySchema>();
        lastEditedBy.Name.ShouldBe("Edited By");
    }

    [Fact]
    public void LastEditedTimePropertySchema_Deserializes()
    {
        var json = """{"id":"let","name":"Edited","type":"last_edited_time","last_edited_time":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var lastEditedTime = schema.ShouldBeOfType<LastEditedTimePropertySchema>();
        lastEditedTime.Name.ShouldBe("Edited");
    }

    [Fact]
    public void UniqueIdPropertySchema_Deserializes()
    {
        var json = """{"id":"uid","name":"ID","type":"unique_id","unique_id":{"prefix":"TASK"}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var uniqueId = schema.ShouldBeOfType<UniqueIdPropertySchema>();
        uniqueId.Name.ShouldBe("ID");
        uniqueId.UniqueId.ShouldNotBeNull();
        uniqueId.UniqueId.Prefix.ShouldBe("TASK");
    }

    [Fact]
    public void ButtonPropertySchema_Deserializes()
    {
        var json = """{"id":"btn","name":"Action","type":"button","button":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var button = schema.ShouldBeOfType<ButtonPropertySchema>();
        button.Name.ShouldBe("Action");
    }

    [Fact]
    public void VerificationPropertySchema_Deserializes()
    {
        var json = """{"id":"ver","name":"Verified","type":"verification","verification":{}}""";

        var schema = JsonSerializer.Deserialize<PropertySchema>(json, JsonOptions);
        var verification = schema.ShouldBeOfType<VerificationPropertySchema>();
        verification.Name.ShouldBe("Verified");
    }
}
