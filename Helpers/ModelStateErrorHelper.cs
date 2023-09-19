using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevsTutorialCenterAPI.Helpers
{
    public static class ModelStateErrorHelper
    {
        public static string GetErrors(ModelStateDictionary modelState)
        {

            var errorList = modelState.SelectMany(x => x.Value.Errors.Select(xx => xx.ErrorMessage));
            return string.Join(" ", errorList);

        }
    }
}
