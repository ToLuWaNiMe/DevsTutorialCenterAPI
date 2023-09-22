using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevsTutorialCenterAPI.Utilities;

public static class ModelStateExtension
{
    public static string GetError(this ModelStateDictionary modelState)
    {
        var errorList = modelState.SelectMany(x => x.Value.Errors.Select(xx => xx.ErrorMessage));
        return string.Join(" ", errorList);
    }
}