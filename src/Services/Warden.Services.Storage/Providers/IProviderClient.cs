﻿using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Mappers;

namespace Warden.Services.Storage.Providers
{
    public interface IProviderClient
    {
        Task<Maybe<T>> GetAsync<T>(string url, string endpoint, IMapper<T> mapper) where T : class;
        Task<Maybe<T>> GetUsingStorageAsync<T>(string url, string endpoint,
            Func<Task<Maybe<T>>> storageFetch, Func<T, Task> storageSave, IMapper<T> mapper) where T : class;
    }
}