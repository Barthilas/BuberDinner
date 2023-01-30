using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BuberDinner.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehaviour(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validator is null)
                return await next();

            var validatioResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validatioResult.IsValid)
                return await next();


            // ConvertAll can be used.
            var errors = validatioResult.Errors
            .ConvertAll(validationFailure => Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));
            // .Select(validationFailure => Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage))
            // .ToList();

            // MAGIC: Error here because compiler doesnt know there is implicit convertor from list of errors
            // to ErrorOr object. 
            return (dynamic)errors;

        }
    }
    #region non-generic
    // public class ValidateRegisterCommandBehaviour : IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
    // {
    //     private readonly IValidator<RegisterCommand> _validator;

    //     public ValidateRegisterCommandBehaviour(IValidator<RegisterCommand> validator)
    //     {
    //         _validator = validator;
    //     }

    //     public async Task<ErrorOr<AuthenticationResult>> Handle(
    //         RegisterCommand request,
    //         RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next,
    //         CancellationToken cancellationToken)
    //     {
    //         var validatioResult = await _validator.ValidateAsync(request, cancellationToken);

    //         if (validatioResult.IsValid)
    //         {
    //             return await next();
    //         }

    //         // ConvertAll can be used.
    //         var errors = validatioResult.Errors
    //         .ConvertAll(validationFailure => Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));
    //         // .Select(validationFailure => Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage))
    //         // .ToList();

    //         return errors;

    //     }
    // }
    #endregion
}