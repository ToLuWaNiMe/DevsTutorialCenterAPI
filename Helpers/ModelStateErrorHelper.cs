using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevsTutorialCenterAPI.Helpers
{
    public static class ModelStateErrorHelper
    {
        public static IEnumerable<string> GetErrors(ModelStateDictionary modelState)
        {
            return modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
        }
    }
}
