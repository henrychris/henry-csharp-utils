﻿using HenryUtils.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HenryUtils.Filters;

public class CustomValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Check if the model state is invalid
        if (context.ModelState.IsValid)
        {
            return;
        }

        // Create a custom response
        var customErrorResponse = new ApiErrorResponse(
            context
                .ModelState.Where(x => x.Value!.Errors.Count > 0)
                .SelectMany(kvp => kvp.Value!.Errors.Select(e => new ApiError { Code = kvp.Key, Description = e.ErrorMessage }))
                .ToList(),
            "One or more validation errors occurred."
        );

        context.Result = new ObjectResult(customErrorResponse) { StatusCode = StatusCodes.Status422UnprocessableEntity };
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No action needed after the action is executed
    }
}
