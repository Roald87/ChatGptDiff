using DiffPlex.DiffBuilder.Model;

namespace ChatGPTDiffApp.Models
{
    public class Message
    {
        public string Role { get; set; } = string.Empty; // Default to empty string
        public string Content { get; set; } = string.Empty; // Default to empty string
        public DiffPaneModel Diff { get; set; } = new DiffPaneModel();
    }
}
