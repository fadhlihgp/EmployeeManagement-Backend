﻿using System.Net;
using EmployeeManagement_Backend.Exceptions;
using EmployeeManagement_Backend.ViewModels.Responses;

namespace EmployeeManagement_Backend.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException e)
        {
            await HandleExceptionAsync(context, e);
            _logger.LogError(e.Message);
        }
        catch (UnauthorizedException e)
        {
            await HandleExceptionAsync(context, e);
            _logger.LogError(e.Message);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
            _logger.LogError(e.Message);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Will display as json format
        context.Response.ContentType = "application/json";
        // Instance error response from Dto
        var errorResponse = new ErrorResponse();

        switch (exception)
        {
            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = exception.Message;
                break;
            case UnauthorizedException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = exception.Message;
                break;
            case not null:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = exception.Message;
                break;
        }

        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}