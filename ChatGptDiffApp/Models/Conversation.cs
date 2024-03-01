using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

namespace ChatGPTDiffApp.Models
{
    public class Conversation : List<Message>
    {
        public Conversation()
            : base() { }

        public List<Message> NormalView() => this;

        public List<Message> DiffView()
        {
            var diffs = new List<Message>();
            Message previous = this.FirstOrDefault(m => m.Role == "assistant") ?? new Message();
            foreach (Message m in this)
            {
                if (m.Role == "user")
                {
                    diffs.Add(m);
                }
                else
                {
                    var diff = InlineDiffBuilder.Diff(previous.Content, m.Content);
                    StringBuilder diffDisplay = new();

                    foreach (var line in diff.Lines)
                    {
                        switch (line.Type)
                        {
                            case ChangeType.Inserted:
                                diffDisplay.Append("<div class='diff-line diff-inserted'>+ ");
                                break;
                            case ChangeType.Deleted:
                                diffDisplay.Append("<div class='diff-line diff-deleted'>- ");
                                break;
                            default:
                                diffDisplay.Append("<div class='diff-line'>");
                                break;
                        }

                        diffDisplay.AppendLine($"{HtmlEncode(line.Text)}</div>");
                    }

                    Message diffMessage = new() { Role = m.Role, Content = diffDisplay.ToString() };
                    diffs.Add(diffMessage);
                    previous = m;
                }
            }
            return diffs;
        }

        static string HtmlEncode(string text) => WebUtility.HtmlEncode(text);
    }
}
