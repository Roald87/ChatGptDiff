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
                    DiffPaneModel diff = InlineDiffBuilder.Diff(previous.Content, m.Content);
                    m.Diff = diff;
                    diffs.Add(m);
                    previous = m;
                }
            }
            return diffs;
        }
    }
}
