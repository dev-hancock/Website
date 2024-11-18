// -----------------------------------------------------------------------
//  <copyright file="ContactCommand.cs" company="Hancock Software Solutions Limited">
//      Copyright (c) Hancock Software Solutions Limited 2024. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Website.Application.Commands
{
    using System.Reactive;
    using Core.Abstractions.Handlers;
    using Core.Abstractions.Messages;
    using FluentValidation;
    using Interfaces;

    public class ContactCommand : Command
    {
        public string Body { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }
    }

    public class ContactCommandValidator : AbstractValidator<ContactCommand>
    {
        public ContactCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(x => x.Subject)
                .NotEmpty()
                .WithMessage("Subject is required.")
                .MaximumLength(100)
                .WithMessage("Subject cannot exceed 100 characters.");

            RuleFor(x => x.Body)
                .NotEmpty()
                .WithMessage("Message body is required.")
                .MaximumLength(1000)
                .WithMessage("Message body cannot exceed 1000 characters");
        }
    }

    public class ContactCommandHandler(IEmailService service, string recipient) : IHandler<ContactCommand>
    {
        private readonly string _recipient = recipient;

        private readonly IEmailService _service = service;

        /// <inheritdoc />
        public async Task<Unit> Handle(ContactCommand message, CancellationToken token = default)
        {
            var content = new[]
            {
                $"Name: {message.Name}",
                $"Email: {message.Email}",
                $"Body: {message.Body}"
            };

            var body = string.Join("\n", content);

            await _service.SendAsync(message.Email, _recipient, message.Subject, body, token);

            return default;
        }
    }
}