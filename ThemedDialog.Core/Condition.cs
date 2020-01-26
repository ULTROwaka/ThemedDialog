namespace ThemedDialog.Core
{
    public class Condition
    {
        public ConditionOperator ComparisonOperator;
        public Variable Variable;
        public object Value;

        public Condition(ConditionOperator comparisonOperator, Variable variable, object value)
        {
            ComparisonOperator = comparisonOperator;
            Variable = variable;
            Value = value;
        }
    }
}