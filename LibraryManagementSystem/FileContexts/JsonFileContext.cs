using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryManagementSystem.FileContexts
{
	public class JsonFileContext<T> : IFileContext<T>
	{
        private static string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LibraryManagementSystem");

        public JsonFileContext()
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public List<T> ReadFromFile(string filePath)
        {
            filePath = Path.Combine(directory, filePath);

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

            var items = JsonSerializer.Deserialize<List<T>>(jsonData, options);
            return items ?? new List<T>();
        }

        public void WriteToFile(string filePath, List<T> items)
        {
            filePath = Path.Combine(directory, filePath);

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

