// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

using System.Text.Json.Serialization;

namespace DamianH.NotionClient.Models.Filters;

/// <summary>
/// Abstract base for all filter types.
/// Use <see cref="CompoundFilter"/> for and/or nesting,
/// <see cref="PropertyFilter"/> for property-based filters,
/// and <see cref="TimestampFilter"/> for created_time/last_edited_time.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
[JsonDerivedType(typeof(CompoundFilter), "compound")]
[JsonDerivedType(typeof(PropertyFilter), "property")]
[JsonDerivedType(typeof(TimestampFilter), "timestamp")]
public abstract class Filter;
