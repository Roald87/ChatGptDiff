using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

namespace ChatGPTDiffApp.Models
{
    public class Conversation : List<Message>
    {
        public Conversation()
            : base() { }

        public List<Message> NormalView() => this;

        public List<DiffMessage> DiffView()
        {
            var diffs = new List<DiffMessage>();
            Message previousMessage =
                this.FirstOrDefault(m => m.Role == "assistant") ?? new Message();
            DiffMessage previous =
                new() { Role = previousMessage.Role, Content = previousMessage.Content };
            foreach (Message m in this)
            {
                DiffMessage dm = new() { Role = m.Role, Content = m.Content };
                if (dm.Role == "user")
                {
                    diffs.Add(dm);
                }
                else
                {
                    DiffPaneModel diff = InlineDiffBuilder.Diff(previous.Content, dm.Content);
                    dm.Diff = diff;
                    diffs.Add(dm);
                    previous = dm;
                }
            }
            return diffs;
        }
    }
}
