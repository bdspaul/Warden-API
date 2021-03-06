﻿using System;

namespace Warden.DTO.Security
{
    public class SecuredRequestDto
    {
        public string ResourceType { get; set; }
        public Guid ResourceId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UsedAt { get; set; }
    }
}