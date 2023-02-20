﻿using Audiochan.Common;
using Audiochan.Core.Features.Users.Commands.UpdatePassword;
using Audiochan.Tests.Common.Mocks;
using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Options;
using Xunit;

namespace Audiochan.Core.UnitTests.Validations.Users
{
    public class UpdatePasswordCommandValidatorTests
    {
        private readonly IValidator<UpdatePasswordCommand> _validator;

        public UpdatePasswordCommandValidatorTests()
        {
            _validator = new UpdatePasswordCommandValidator();
        }

        [Fact]
        public void PasswordRequired()
        {
            var req = new UpdatePasswordCommand("", "", ClaimsPrincipalMock.CreateFakeUser());
            var validationResult = _validator.TestValidate(req);
            validationResult
                .ShouldHaveValidationErrorFor(x => x.NewPassword);
        }

        [Theory]
        [InlineData("thisdoesnothavedigits")]
        public void PasswordRequireDigits(string password)
        {
            var req = new UpdatePasswordCommand(password, "", ClaimsPrincipalMock.CreateFakeUser());

            var validationResult = _validator.TestValidate(req);

            validationResult
                .ShouldHaveValidationErrorFor(x => x.NewPassword)
                .WithErrorCode(ValidationErrorCodes.Password.DIGITS);
        }

        [Theory]
        [InlineData("OMEGALUL4HEAD")]
        public void PasswordRequireLowercase(string password)
        {
            var req = new UpdatePasswordCommand(password, "", ClaimsPrincipalMock.CreateFakeUser());

            var validationResult = _validator.TestValidate(req);

            validationResult
                .ShouldHaveValidationErrorFor(x => x.NewPassword)
                .WithErrorCode(ValidationErrorCodes.Password.LOWERCASE);
        }

        [Theory]
        [InlineData("omegalul4head")]
        public void PasswordRequireUppercase(string password)
        {
            var req = new UpdatePasswordCommand(password, "", ClaimsPrincipalMock.CreateFakeUser());

            var validationResult = _validator.TestValidate(req);

            validationResult
                .ShouldHaveValidationErrorFor(x => x.NewPassword)
                .WithErrorCode(ValidationErrorCodes.Password.UPPERCASE);
        }

        [Theory]
        [InlineData("lkdsfhlksdjflksdjflks")]
        public void PasswordRequireNonAlphanumeric(string password)
        {
            var req = new UpdatePasswordCommand(password, "", ClaimsPrincipalMock.CreateFakeUser());

            var validationResult = _validator.TestValidate(req);

            validationResult
                .ShouldHaveValidationErrorFor(x => x.NewPassword)
                .WithErrorCode(ValidationErrorCodes.Password.NON_ALPHANUMERIC);
        }

        [Theory]
        [InlineData("no")]
        public void PasswordRequireLength(string password)
        {
            var req = new UpdatePasswordCommand(password, "", ClaimsPrincipalMock.CreateFakeUser());

            var validationResult = _validator.TestValidate(req);

            validationResult
                .ShouldHaveValidationErrorFor(x => x.NewPassword)
                .WithErrorCode(ValidationErrorCodes.Password.LENGTH);
        }
    }
}