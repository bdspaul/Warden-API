﻿using System.Threading.Tasks;
using Rebus.Bus;
using Rebus.Handlers;
using Warden.Services.RealTime.Events;

namespace Warden.Services.Stats.Handlers.Events
{
    public class WardenCheckResultProcessedHandler : IHandleMessages<WardenCheckResultProcessed>
    {
        private readonly IBus _bus;

        public WardenCheckResultProcessedHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(WardenCheckResultProcessed message)
        {
        }
    }
}