namespace S2_HW9
{
    using System;
    using System.IO;
    using System.Text.Json;
    using System.Xml;
    using System.Xml.Linq;

    internal class Program
    {
        static void Main(string[] args)
        {
            // Получаем текущую директорию
            string currentDirectory = Directory.GetCurrentDirectory();
            string jsonFilePath = Path.Combine(currentDirectory, "input.json");
            string xmlFilePath = Path.Combine(currentDirectory, "output.xml");

            try
            {
                // Читаем JSON файл
                string jsonString = File.ReadAllText(jsonFilePath);

                // Конвертируем JSON в XML
                XDocument xmlDocument = JsonToXml(jsonString);

                // Сохраняем XML файл
                xmlDocument.Save(xmlFilePath);

                Console.WriteLine("Конвертация завершена. Файл сохранен как output.xml.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        static XDocument JsonToXml(string jsonString)
        {
            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                return JsonToXml(doc.RootElement);
            }
        }

        static XDocument JsonToXml(JsonElement element)
        {
            XElement xElement = new XElement("Root");
            ConvertJsonElementToXml(element, xElement);
            return new XDocument(xElement);
        }

        static void ConvertJsonElementToXml(JsonElement element, XElement xElement)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    foreach (JsonProperty property in element.EnumerateObject())
                    {
                        XElement childElement = new XElement(property.Name);
                        ConvertJsonElementToXml(property.Value, childElement);
                        xElement.Add(childElement);
                    }
                    break;

                case JsonValueKind.Array:
                    foreach (JsonElement arrayElement in element.EnumerateArray())
                    {
                        XElement arrayChild = new XElement("Item");
                        ConvertJsonElementToXml(arrayElement, arrayChild);
                        xElement.Add(arrayChild);
                    }
                    break;

                case JsonValueKind.String:
                    xElement.Value = element.GetString();
                    break;

                case JsonValueKind.Number:
                    xElement.Value = element.GetDouble().ToString();
                    break;

                case JsonValueKind.True:
                case JsonValueKind.False:
                    xElement.Value = element.GetBoolean().ToString();
                    break;

                case JsonValueKind.Null:
                    xElement.Value = string.Empty;
                    break;
            }
        }
    }
}