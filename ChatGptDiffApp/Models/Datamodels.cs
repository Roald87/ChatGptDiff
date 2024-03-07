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
        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    public class DiffMessage : IMessage
    {
        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public DiffPaneModel Diff { get; set; } = new();
    }
}
