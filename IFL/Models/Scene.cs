using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFL.Models;

public class Scene
{
    public string Name { get; set; }
    public List<string> Characters { get; set; }
    public string Text { get; set; }
    public List<Option> Options { get; set; }
    
    public Scene()
    {
        Text = string.Empty;
        Characters = new List<string>();
        Options = new List<Option>();
    }
}
