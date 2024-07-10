using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.FileContexts
{
	public class JsonFileContext<T> : IFileContext<T>
	{
		public List<T> ReadFromFile(string filePath) {

			if (!File.Exists(filePath))
			{
                Console.WriteLine("File not found. Returning default content.");
                return new List<T>();
            }

            var jsonData = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            };

            return JsonSerializer.Deserialize<List<T>>(jsonData, options);
		}

        public void WriteToFile(string filePath, List<T> items)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };

            string jsonString = JsonSerializer.Serialize(items, options);

            File.WriteAllText(filePath, jsonString);
        }

	}
}

