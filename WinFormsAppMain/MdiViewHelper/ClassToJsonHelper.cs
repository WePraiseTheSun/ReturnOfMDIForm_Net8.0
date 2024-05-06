using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;


namespace MdiViewHelper
{

    // Static helper class for JSON operations, with flexible handling for evolving data models.
    public static class ClassToJsonHelper
    {
        // Serializer settings to ignore missing member fields when deserializing.
        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Formatting = Newtonsoft.Json.Formatting.Indented,
            ObjectCreationHandling = ObjectCreationHandling.Replace
        };

        // Loads JSON from a file and deserializes it into the specified type, ignoring unmatched fields.
        public static T LoadFromFile<T>(string filePath) where T : new()
        {
            try
            {
                if (!File.Exists(filePath)) return new T();
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not load from file.", ex);
            }
        }

        // Serializes an object and saves it to a file as JSON, including new fields.
        public static void SaveToFile<T>(string filePath, T obj)
        {
            try
            {
                string json = JsonConvert.SerializeObject(obj, settings);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not save to file.", ex);
            }
        }
    }

}
