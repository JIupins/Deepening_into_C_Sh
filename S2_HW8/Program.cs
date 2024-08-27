namespace S2_HW8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: dotnet run [extension] [text]. For example: dotnet run cs using");
                return;
            }

            string extension = args[0];
            string searchText = args[1];

            string currentDirectory = Directory.GetCurrentDirectory();
            SearchFiles(currentDirectory, extension, searchText);
        }

        static void SearchFiles(string directory, string extension, string searchText)
        {
            try
            {
                string[] files = Directory.GetFiles(directory, $"*.{extension}");

                foreach (string file in files)
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string fileContent = reader.ReadToEnd();
                        if (fileContent.Contains(searchText))
                        {
                            Console.WriteLine($"Found text '{searchText}' in file: {file}");
                        }
                    }
                }

                string[] subdirectories = Directory.GetDirectories(directory);
                foreach (string subdirectory in subdirectories)
                {
                    SearchFiles(subdirectory, extension, searchText);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
    }
}