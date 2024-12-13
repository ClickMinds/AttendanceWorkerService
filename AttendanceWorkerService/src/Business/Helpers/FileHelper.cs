namespace AttendanceWorkerService.src.Business.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// Creates or overwrites a file with the given content.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="lines">The lines of text to write to the file.</param>
        public static async Task WriteToFileAsync(string filePath, IEnumerable<string> lines)
        {
            try
            {
                using var writer = new StreamWriter(filePath, false);
                foreach (var line in lines)
                {
                    await writer.WriteLineAsync(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Appends content to an existing file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="lines">The lines of text to append to the file.</param>
        public static async Task AppendToFileAsync(string filePath, IEnumerable<string> lines)
        {
            try
            {
                using var writer = new StreamWriter(filePath, true);
                foreach (var line in lines)
                {
                    await writer.WriteLineAsync(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while appending to the file: {ex.Message}");
                throw;
            }
        }
    }
}
