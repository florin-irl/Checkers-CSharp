using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CheckersCSharp.Models;
using CheckersCSharp.Models.Pieces;

namespace CheckersCSharp.Services
{
    public class PieceConverter : JsonConverter<Piece>
    {
        public override Piece Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                var type = root.GetProperty("Type").GetInt32();
                var color = root.GetProperty("Color").GetInt32();
                switch (type)
                {
                    case 0:
                        return new Soldier((EPlayer)color);
                    case 1:
                        return new King((EPlayer)color);
                    default:
                        throw new JsonException();
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, Piece value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
