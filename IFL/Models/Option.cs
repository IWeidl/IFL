namespace IFL.Models;

public class Option
{
    public string TargetScene { get; set; }
    public string Text { get; set; }
    public List<Condition> Conditions { get; set; }

    public Option()
    {
        Text = string.Empty;
        Conditions = new List<Condition>();
    }
}