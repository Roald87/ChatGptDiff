using DiffPlex.DiffBuilder.Model;

namespace ChatGPTDiffApp.Models
{
    public interface IMessage
    {
        string Role { get; set; }
        string Content { get; set; }
    }

    public class Message : IMessage
    {
        internal Message() { }

        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    public class UserMessage
    {
        public static Message Create(string content)
        {
            return new Message { Role = "user", Content = content };
        }
    }

    public class AssistantMessage
    {
        public static Message Create(string content)
        {
            return new Message { Role = "assistant", Content = content };
        }
    }

    public class DiffMessage : IMessage
    {
        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public DiffPaneModel Diff { get; set; } = new();
    }
}
