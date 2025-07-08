using System.Threading.Tasks;
using OpenAI_API;

namespace PlantDoctorApp
{
    public class OpenAIService
    {
        private readonly OpenAIAPI _api;

        public OpenAIService()
        {
            var key = System.Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            _api = new OpenAIAPI(key);
        }

        public async Task<string> GetAdviceAsync(string plantName, string condition)
        {
            string prompt = $"Provide care advice for {plantName} with condition {condition}.";
            var result = await _api.Chat.CreateChatCompletionAsync(new OpenAI_API.Chat.ChatRequest()
            {
                Model = OpenAI_API.Models.Model.GPT4,
                Temperature = 0.7,
                MaxTokens = 100,
                Messages = new System.Collections.Generic.List<OpenAI_API.Chat.ChatMessage>
                {
                    new OpenAI_API.Chat.ChatMessage(OpenAI_API.Chat.ChatMessageRole.User, prompt)
                }
            });
            return result.Choices[0].Message.Content.Trim();
        }
    }
}
