﻿using Audiochan.Core.Features.Auth.Errors;
using Audiochan.Core.Features.Auth.Models;
using OneOf;

namespace Audiochan.Core.Features.Auth.Results;

[GenerateOneOf]
public partial class LoginResult : OneOfBase<AuthTokenResult, LoginWithPasswordFailed>
{
    
}