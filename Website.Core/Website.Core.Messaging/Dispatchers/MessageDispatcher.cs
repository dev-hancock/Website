// -----------------------------------------------------------------------
//  <copyright file="MessageDispatcher.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Messaging.Dispatchers
{
    using System.Reflection;
    using Abstractions.Handlers;
    using Abstractions.Messages.Interfaces;
    using Abstractions.Pipeline;

    public abstract class MessageDispatcher(IServiceProvider services) : IDispatcher
    {
        /// <inheritdoc />
        public abstract bool CanHandle<T>(IMessage<T> message);

        /// <inheritdoc />
        public abstract Task<T?> Dispatch<T>(IMessage<T> message, CancellationToken token = default);

        protected static async Task<T?> Invoke<T>(object? handler, IMessage<T> message, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            if (handler != null)
            {
                if (GetHandleMethod(handler, message)?.Invoke(handler, [message]) is Task<T> task)
                {
                    return await task;
                }
            }

            return default;
        }

        protected object? GetHandler<T>(Type message)
        {
            return services.GetService(GetHandlerType(message, typeof(T)));
        }

        protected IEnumerable<object?> GetHandlers(Type message)
        {
            return (IEnumerable<object>)services.GetService(GetHandlersType(message))!;
        }

        private static bool AcceptsType(MethodInfo method, Type message)
        {
            return method.GetParameters().Any(x => x.ParameterType == message);
        }

        private static MethodInfo? GetHandleMethod<T>(object? handler, IMessage<T> message)
        {
            return GetHandleMethods<T>(handler)?.FirstOrDefault(x => AcceptsType(x, message.GetType()));
        }

        private static IEnumerable<MethodInfo>? GetHandleMethods<T>(object? handler)
        {
            return handler?.GetType().GetMethods().Where(IsHandleMethod<T>);
        }

        private static bool IsHandleMethod<T>(MethodInfo? info)
        {
            return info?.Name == nameof(IHandler<IMessage<T>, T>.Handle);
        }

        private Type GetHandlersType(Type message)
        {
            return typeof(IEnumerable<>).MakeGenericType(GetHandlerType(message));
        }

        private Type GetHandlerType(Type message, Type result)
        {
            return typeof(IHandler<,>).MakeGenericType(message, result);
        }

        private Type GetHandlerType(Type message)
        {
            return typeof(IHandler<>).MakeGenericType(message);
        }
    }
}