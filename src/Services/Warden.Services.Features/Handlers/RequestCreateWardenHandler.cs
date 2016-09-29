﻿using System.Threading.Tasks;
using RawRabbit;
using Warden.Common.Commands;
using Warden.Common.Commands.Wardens;
using Warden.Services.Features.Domain;
using Warden.Services.Features.Services;

namespace Warden.Services.Features.Handlers
{
    public class RequestCreateWardenHandler : ICommandHandler<RequestCreateWarden>
    {
        private readonly IBusClient _bus;
        private readonly IUserFeaturesManager _userFeaturesManager;

        public RequestCreateWardenHandler(IBusClient bus, 
            IUserFeaturesManager userFeaturesManager)
        {
            _bus = bus;
            _userFeaturesManager = userFeaturesManager;
        }

        public async Task HandleAsync(RequestCreateWarden command)
        {
            var featureAvailable = await _userFeaturesManager
                .IsFeatureIfAvailableAsync(command.UserId, FeatureType.AddWarden);
            if (!featureAvailable)
                return;

            await _bus.PublishAsync(new CreateWarden
            {
                UserId = command.UserId,
                Name = command.Name,
                WardenId = command.WardenId,
                OrganizationId = command.OrganizationId,
                Enabled = command.Enabled
            });
        }
    }
}