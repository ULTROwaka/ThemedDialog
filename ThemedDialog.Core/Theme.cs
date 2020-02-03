using System;
using System.Collections;
using System.Collections.Generic;

namespace ThemedDialog.Core
{
    public class Theme
    {
        public string Name;

        public Theme(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ThemeComparer : IComparer<Theme>
    {
        public int Compare(Theme x, Theme y)
        {
            return new CaseInsensitiveComparer().Compare(x.Name, y.Name);
        }
    }
}
