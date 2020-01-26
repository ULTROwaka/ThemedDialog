using System.Collections.Generic;

namespace ThemedDialog.Core
{
    public class Sentence
    {
        public List<Condition> Condtions;
        public string Text;

        public Sentence(IEnumerable<Condition> condtions, string text)
        {
            Condtions = new List<Condition>(condtions);
            Text = text;
        }
    }
}