using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.ErrorModels;

internal class ValidationErrorReponse : ModelStateDictionary
{
    public IEnumerable<ValidationError> Errors { get; set; }
}