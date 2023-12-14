using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFL.Models;

namespace IFL
{
    internal class Engine
    {
        public List<Scene> Scenes { get; set; }
        public List<Variable> Variables { get; set; }

        private string _filePath;
        private string _fileContents;

        public Engine()
        {
            Scenes = new List<Scene>();
            Variables = new List<Variable>();
        }

        public void LoadScenesFromFile(string filePath)
        {
            _filePath = filePath;
            _fileContents = File.ReadAllText(filePath);

            var rawSceneStrings = _fileContents.Split(":::\r\n").ToList();
            Scenes = Parser.ParseScenes(rawSceneStrings);
        }
    }
}
