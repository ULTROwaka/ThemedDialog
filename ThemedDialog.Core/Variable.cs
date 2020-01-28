using System;
using System.Text;

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

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[").Append(Type.Name).Append("]")
                .Append(" ").Append(Name);
            return stringBuilder.ToString();
        }
    }
}