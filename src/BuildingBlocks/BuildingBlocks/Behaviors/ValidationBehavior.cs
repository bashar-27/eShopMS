using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;


namespace BuildingBlocks.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TRequest>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult = await Task.WhenAll(validators.Select(val => val.ValidateAsync(context, cancellationToken)));
            var failures = validationResult.Where(res => res.Errors.Any()).SelectMany(res => res.Errors).ToList();
            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
            return await next();
        }

    }
}
