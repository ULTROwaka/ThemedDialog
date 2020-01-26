using System;

namespace ThemedDialog.Core
{
    public class Variable
    {
        public string Name;
        public Type Type;

        public Variable(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}