﻿using Blazored.LocalStorage;

namespace ManagementSolution.Client.Helpers
{
    public class LocalStorageService(ILocalStorageService localStorageService)
    {
        private readonly ILocalStorageService _localStorageService = localStorageService;
        private const string StorageKey = "authentication-token";

        public async Task<string> GetToken() => await _localStorageService.GetItemAsStringAsync(StorageKey);
        public async Task SetToken(string item) => await _localStorageService.SetItemAsStringAsync(StorageKey, item);
        public async Task RemoveToken() => await _localStorageService.RemoveItemAsync(StorageKey);
    }
}
