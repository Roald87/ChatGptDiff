using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using ChatGPTDiffApp.Models;

namespace ChatGPTDiffApp.Services
{
    public class ChatGPTService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private const string OpenAiEndpoint = "https://api.openai.com/v1/chat/completions";

        public ChatGPTService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<string> GetResponseAsync(List<Message> conversation)
        {
            string apiKey = await _localStorage.GetItemAsStringAsync("apiKey") ?? string.Empty;
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            // The request payload now includes the entire conversation history
            var data = new { model = "gpt-3.5-turbo", messages = conversation };

            var response = await _httpClient.PostAsJsonAsync(OpenAiEndpoint, data);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadFromJsonAsync<JsonElement>();

            var choices = responseJson.GetProperty("choices");
            if (choices.GetArrayLength() > 0)
            {
                var firstChoice = choices[0];
                var message = firstChoice.GetProperty("message");
                return message.GetProperty("content").GetString() ?? string.Empty;
            }

            return "No response";
        }
    }
}
