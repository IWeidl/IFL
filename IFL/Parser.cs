using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFL.Models;

namespace IFL
{
    internal static class Parser
    {
        internal static Scene ParseScene(string rawSceneString)
        {
            Console.WriteLine("Parsing scene...");
            using var reader = new StringReader(rawSceneString);
            string? line;
            var newScene = new Scene();

            while ((line = reader.ReadLine()) != null)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (line.StartsWith("Name:"))
                {
                    if (newScene.Name != null)
                        throw new Exception("Multiple 'Name:' entries found in scene");
                    newScene.Name = line.Replace("Name:", "").Trim();
                }
                else if (line.StartsWith("Characters:"))
                {
                    if (newScene.Characters?.Count != 0)
                        throw new Exception("Multiple 'Characters:' entries found in scene");

                    newScene.Characters = line.Replace("Characters:", "").Split(",").Select(c => c.Trim()).ToList();
                }
                else if (line.StartsWith("Text:"))
                {
                    if (!string.IsNullOrWhiteSpace(newScene.Text))
                        throw new Exception("Multiple 'Text:' entries found in scene");

                    newScene.Text = line.Replace("Text:", "").Trim();
                }
                else if (line.StartsWith("Option"))
                {
                    if (newScene.Options.Any())
                        throw new Exception("Multiple 'Option' entries found in scene");

                    newScene.Options = ParseOptions(reader);
                }
            }

            PrintScene(newScene);

            return newScene;
        }


        internal static List<Scene> ParseScenes(List<string> rawSceneStrings)
        {
            var scenes = new List<Scene>();
            foreach (var rawSceneString in rawSceneStrings)
            {
                scenes.Add(ParseScene(rawSceneString));
            }

            return scenes;
        }

        private static List<Option> ParseOptions(StringReader reader)
        {
            var options = new List<Option>();
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line) || line.Trim() == "[")
                    continue;

                if (line.Trim() == "]")
                    break;

                var parts = line.Trim().Split(':', 2);
                if (parts.Length != 2 || parts[1].Trim() != "{")
                    throw new Exception($"Invalid option format: {line}");

                var newOption = new Option();
                newOption.TargetScene = parts[0].Trim();
                
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line == "}")
                        break;

                    if (line.Contains("Text:"))
                        newOption.Text = line.Replace("Text:", "").Trim();
                    else if (line.Contains("Conditions:"))
                        newOption.Conditions = ParseConditions(reader);
                }
                
                options.Add(newOption);
            }

            return options;
        }

        private static List<Condition> ParseConditions(StringReader reader)
        {
            return new List<Condition>();
        }

        private static void PrintScene(Scene scene)
        {
            Console.WriteLine($"Scene name: {scene.Name}");
            Console.WriteLine($"Characters: {string.Join(", ", scene.Characters)}");
            Console.WriteLine($"Text: {scene.Text}");
            Console.WriteLine($"Options:");
            foreach (var option in scene.Options)
            {
                Console.WriteLine($"--Target scene: {option.TargetScene}");
                Console.WriteLine($"--Text: {option.Text}");
            }
        }
    }
}
