using Microsoft.AspNetCore.Mvc;

internal class ApiBehaviorOptions
{
    public Func<object, BadRequestObjectResult> InvalidModelStateResponseFactory { get; internal set; }
}