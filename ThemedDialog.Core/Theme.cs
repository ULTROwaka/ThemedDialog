﻿using System;

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
}
