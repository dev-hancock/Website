// -----------------------------------------------------------------------
//  <copyright file="ValidationBehaviour.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Infrastructure.Behaviours
{
    using Core.Abstractions.Messages.Interfaces;
    using Core.Abstractions.Pipeline;
    using FluentValidation;

    public class ValidationBehaviour(IServiceProvider services) : IBehaviour
    {
        private readonly IServiceProvider _services = services;

        /// <inheritdoc />
        public async Task<T?> Invoke<T>(IMessage<T> message, IDispatcher inner, CancellationToken token = default)
        {
            if (GetValidator(message) is { } validator)
            {
                var context = GetValidationContext(message);

                var result = await validator.ValidateAsync(context, token);

                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }
            }

            return await inner.Dispatch(message, token);
        }

        private static IValidationContext GetValidationContext(object message)
        {
            var type = typeof(ValidationContext<>).MakeGenericType(message.GetType());

            return (IValidationContext)Activator.CreateInstance(type, message)!;
        }

        private IValidator? GetValidator(object message)
        {
            var type = typeof(IValidator<>).MakeGenericType(message.GetType());

            return (IValidator?)_services.GetService(type);
        }
    }
}