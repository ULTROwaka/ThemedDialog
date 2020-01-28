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
            if (dialogs == null)
            {
                Dialogs = new List<Dialog>();
            }
            else
            {
                Dialogs = new List<Dialog>(dialogs);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
