using System.Collections.Generic;

namespace ThemedDialog.Core.Control
{
    public interface IConditionChecker
    {
        bool Check(IEnumerable<Condition> conditions);
    }
}