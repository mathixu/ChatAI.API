using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace ChatAI.API.Filters;

public class ValidationModelFilterAttribute : ActionFilterAttribute
{
    private readonly Type? _validatorType;

    public ValidationModelFilterAttribute(Type validatorType)
        : base()
    {
        if (!typeof(IValidator).IsAssignableFrom(validatorType))
        {
            throw new ArgumentException("The validator type must implement IValidator interface", nameof(validatorType));
        }

        _validatorType = validatorType;
    }

    public ValidationModelFilterAttribute()
    {
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelState(context);
            return;
        }

        if (_validatorType is not null)
            UseValidator(context);

        base.OnActionExecuting(context);
    }

    private void UseValidator(ActionExecutingContext context)
    {
        var serviceProvider = context.HttpContext.RequestServices;

        var validator = serviceProvider.GetService(_validatorType!) as IValidator ?? throw new ArgumentException($"Validator {nameof(_validatorType)} not found");

        foreach (var arg in context.ActionArguments)
        {
            if (arg.Value is not null && !arg.Value.GetType().IsClass)
            {
                continue;
            }

            var validationContext = new ValidationContext<object>(arg.Value ?? throw new ArgumentException("Value not found"));
            var validationResult = validator.Validate(validationContext);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                HandleInvalidModelState(context);
                return;
            }
        }
    }

    private void HandleInvalidModelState(ActionExecutingContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);
    }
}
