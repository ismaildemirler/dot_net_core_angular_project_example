using DershaneBul.Entities.Abstract;
using DershaneBul.Entities.Containers.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DershaneBul.Core.NetCore.ActionFilters
{
    public class ValidationFilterAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
       {
            // execute any code before the action executes
            if (context.HttpContext.Request.Method == "POST")
            {
                var param = context.ActionArguments.SingleOrDefault(p => p.Value is IDto);

                if (param.Value == null)
                {
                    context.Result = new BadRequestObjectResult("Aksiyona bir model gönderilmelidir!");
                    return;
                }
                if (!context.ModelState.IsValid)
                {
                    context.Result = new BadRequestObjectResult(GetErrorResponse(context));
                    return;
                }
            }

            await next();

            // execute any code after the action executes
        }

        private static BaseResponse GetErrorResponse(ActionContext context)
        {
            return new BaseResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Lütfen tüm gerekli alanları doldurunuz!",
                Details = context.ModelState.Values.SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .Where(x => !string.IsNullOrEmpty(x)).ToList()
            };
        }
    }
}
