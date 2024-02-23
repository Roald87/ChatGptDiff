using System.Net.Http.Json;
using Blazored.LocalStorage;
using System.Text.Json;

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

        public async Task<string> GetResponseAsync(string userPrompt)
        {
            string apiKey = await _localStorage.GetItemAsStringAsync("apiKey");
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            // Append instructions to the user's prompt
            string promptWithInstructions =
                userPrompt
                + " Respond only with the things the user asked. Do not give any extra explanation. Be brief.";

            var data = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = "You are a helpful assistant. Respond only with the things the user asked. Do not give any extra explanation. Be brief."
                    },
                    new { role = "user", content = promptWithInstructions }
                }
            };
            var response = await _httpClient.PostAsJsonAsync(OpenAiEndpoint, data);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadFromJsonAsync<JsonElement>();

            // Correctly access the 'choices' property
            var choices = responseJson.GetProperty("choices");
            if (choices.GetArrayLength() > 0)
            {
                var firstChoice = choices[0];
                var message = firstChoice.GetProperty("message");
                return message.GetProperty("content").GetString();
            }

            return "No response";
        }
    }
}
