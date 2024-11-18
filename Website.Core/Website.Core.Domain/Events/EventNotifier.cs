// -----------------------------------------------------------------------
//  <copyright file="EventNotifier.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Core.Domain.Events
{
    using Abstractions.Events;
    using Abstractions.Models;
    using Abstractions.Pipeline;

    public class EventPublisher(IMediator mediator) : IEventPublisher
    {
        private readonly IMediator _mediator = mediator;

        /// <inheritdoc />
        public void Raise(Aggregate aggregate)
        {
            if (aggregate?.Events.Any() ?? false)
            {
                foreach (var e in aggregate.Events)
                {
                    _mediator.Dispatch(e);
                }

                aggregate.Events.Clear();
            }
        }
    }
}