using ChatGPTDiffApp.Models;
using DiffPlex.DiffBuilder.Model;
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
            Assert.False(diffView[1].Diff.HasDifferences);
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
            Assert.True(diffView[3].Diff.HasDifferences);
            Assert.Equal(ChangeType.Deleted, diffView[3].Diff.Lines[0].Type);
            Assert.Equal(ChangeType.Inserted, diffView[3].Diff.Lines[1].Type);
        }
    }
}
