using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CheckersCSharp.Models;

namespace CheckersCSharp.Services
{
    public class JSONUtility
    {
        public static void SerializeGame(GameConfiguration game, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var jsonString = JsonSerializer.Serialize(game, options);
            File.WriteAllText(filePath,jsonString);
        }

        public static GameConfiguration DeserializeGame(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new PieceConverter());
            return JsonSerializer.Deserialize<GameConfiguration>(jsonString, options);
        }
    }
}
