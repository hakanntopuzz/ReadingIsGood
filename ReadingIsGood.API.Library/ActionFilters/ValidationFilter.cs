using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReadingIsGood.Domain.Models;
using System.Linq;
using System.Text;

namespace ReadingIsGood.API.ActionFilters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState.Where(p => p.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(y => y.ErrorMessage)).ToArray();

                var errorMessageBuilder = new StringBuilder();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        errorMessageBuilder.Append($"FieldName: {error.Key}, Message: {subError} ");
                    }
                }

                context.Result = new BadRequestObjectResult(ServiceResult.Error(errorMessageBuilder.ToString()));
            }
        }
    }
}