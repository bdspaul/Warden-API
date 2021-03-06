﻿using System;

namespace Warden.Common.Commands.Organizations
{
    public class RequestNewOrganization : IFeatureRequestCommand
    {
        public Request Request { get; set; }
        public string UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}