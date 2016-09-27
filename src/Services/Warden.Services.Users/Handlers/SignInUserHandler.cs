﻿using System;
using System.Threading.Tasks;
using NLog;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.Users;
using Warden.Common.Events.Users;
using Warden.Services.Users.Auth0;
using Warden.Services.Users.Services;

namespace Warden.Services.Users.Handlers
{
    public class SignInUserHandler : ICommandHandler<SignInUser>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IUserService _userService;
        private readonly IApiKeyService _apiKeyService;
        private readonly IAuth0RestClient _auth0RestClient;
        private readonly IBusClient _bus;

        public SignInUserHandler(IUserService userService,
            IApiKeyService apiKeyService,
            IAuth0RestClient auth0RestClient,
            IBusClient bus)
        {
            _userService = userService;
            _apiKeyService = apiKeyService;
            _auth0RestClient = auth0RestClient;
            _bus = bus;
        }

        public async Task HandleAsync(SignInUser command)
        {
            var auth0User = await _auth0RestClient.GetUserByAccessTokenAsync(command.AccessToken);
            var user = await _userService.GetAsync(auth0User.UserId);
            var userId = string.Empty;
            if (user.HasNoValue)
            {
                await _userService.CreateAsync(auth0User.Email, auth0User.UserId);
                user = await _userService.GetAsync(auth0User.UserId);
                userId = user.Value.UserId;
                await _apiKeyService.CreateAsync(Guid.NewGuid(), userId);
                await _bus.PublishAsync(new UserCreated(user.Value.Email, userId,
                    user.Value.Role, user.Value.State, user.Value.CreatedAt));
            }
            userId = user.Value.UserId;
            await _bus.PublishAsync(new UserSignedIn(userId));
        }
    }
}