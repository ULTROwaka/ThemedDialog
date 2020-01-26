using System;
using System.Collections.Generic;
using System.Text;

namespace ThemedDialog.Core
{
    public class DialogCharacter
    {
        public string Name;
        public List<Dialog> Dialogs;

        public DialogCharacter(string name, IEnumerable<Dialog> dialogs)
        {
            Name = name;
            Dialogs = new List<Dialog>(dialogs);
        }
    }
}
