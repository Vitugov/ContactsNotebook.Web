﻿using ContactsNotebook.Lib.Models.Identity;
using Newtonsoft.Json;
using System.Text;

namespace ContactsNotebook.Lib.Services.ApiClients.Authentication
{
    public class AuthenticationApiClient : IAuthenticationApiClient
    {
        private readonly HttpClient _httpClient;

        public AuthenticationApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationApiClient");
        }

        public async Task<bool> RegisterUserAsync(RegisterViewModel registerViewModel)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(registerViewModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"Authentication/Register", jsonContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<TokenResponse?> LoginUserAsync(LoginViewModel loginViewModel)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(loginViewModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"Authentication/Login", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
            return tokenResponse;
        }

        public async Task<bool> LogoutUserAsync()
        {
            var response = await _httpClient.PostAsync($"Authentication/Logout", new StringContent(""));
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async Task<string> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync($"Authentication/GetUsers");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"Authentication/Delete/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
