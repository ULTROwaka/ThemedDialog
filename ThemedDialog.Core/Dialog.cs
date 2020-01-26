using System;
using System.Collections.Generic;
using System.Text;

namespace ThemedDialog.Core
{
    public class Dialog
    {
        public Theme Theme;
        public List<Sentence> Sentences;

        public Dialog(Theme theme, IEnumerable<Sentence> sentences)
        {
            Theme = theme;
            Sentences = new List<Sentence>(sentences);
        }
    }
}
