﻿using System.Net;

namespace Catalog.Application.Exceptions;

public class ProductNotFoundException : BaseException
{
    public ProductNotFoundException(string id)
        : base($"Product with id {id} not found", HttpStatusCode.NotFound)
    {
    }
}