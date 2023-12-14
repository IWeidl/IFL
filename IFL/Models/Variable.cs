namespace IFL.Models;

public class Variable
{
    public string Identifier { get; set; }
    public VariableType VariableType { get; set; }
    public string Value { get; set; }
}

public enum VariableType
{
    String,
    Integer,
    Float,
    Boolean,
}