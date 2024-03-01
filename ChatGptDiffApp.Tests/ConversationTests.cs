using System.Linq;
using ChatGPTDiffApp.Models;
using Xunit;

namespace DiffChat.Tests
{
    public class ConversationTests
    {
        [Fact]
        public void EmptyConversation_NormalAndDiffView_AreEmpty()
        {
            var conversation = new Conversation();

            Assert.Empty(conversation.NormalView());
            Assert.Empty(conversation.DiffView());
        }

        [Fact]
        public void ConversationWithOneUserOneAssistant_NormalAndDiffView_CheckContents()
        {
            var conversation = new Conversation
            {
                new Message { Role = "user", Content = "Hello" },
                new Message { Role = "assistant", Content = "Hi there!" }
            };

            var normalView = conversation.NormalView();
            Assert.Equal(2, normalView.Count);
            Assert.Equal("Hello", normalView[0].Content);
            Assert.Equal("Hi there!", normalView[1].Content);

            var diffView = conversation.DiffView();
            Assert.Equal(2, diffView.Count);
            Assert.Equal("Hello", normalView[0].Content);
            Assert.Contains("<div>Hi there!</div>", diffView[1].Content);
        }

        [Fact]
        public void ConversationWithTwoUserTwoAssistant_NormalAndDiffView_CheckContents()
        {
            var conversation = new Conversation
            {
                new Message { Role = "user", Content = "How are you?" },
                new Message { Role = "assistant", Content = "I'm good, thanks!" },
                new Message { Role = "user", Content = "Tell me a joke." },
                new Message { Role = "assistant", Content = "I'm good thanks?" }
            };

            var normalView = conversation.NormalView();
            Assert.Equal(4, normalView.Count);

            var diffView = conversation.DiffView();
            Assert.Equal(4, diffView.Count);
            Assert.Contains("<div style='color:green;'>+", diffView[3].Content);
            Assert.Contains("<div style='color:red;'>-", diffView[3].Content);
            Console.WriteLine(diffView[1].Content);
        }
    }
}
