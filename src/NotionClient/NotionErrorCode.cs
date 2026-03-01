// Copyright (c) Damian Hickey. All rights reserved.
// See LICENSE in the project root for license information.

namespace DamianH.NotionClient;

public enum NotionErrorCode
{
    Unauthorized,
    RestrictedResource,
    ObjectNotFound,
    RateLimited,
    InvalidJson,
    InvalidRequestUrl,
    InvalidRequest,
    ValidationError,
    ConflictError,
    InternalServerError,
    ServiceUnavailable,
}
