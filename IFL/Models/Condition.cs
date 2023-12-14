namespace IFL.Models;

public class Condition
{
    public Variable Variable { get; set; }
    public ConditionType ConditionType { get; set; }
    public string Value { get; set; }
}

public enum ConditionType
{
    Equals,
    NotEquals,
    GreaterThan,
    LessThan,
    GreaterThanOrEquals,
    LessThanOrEquals,
}