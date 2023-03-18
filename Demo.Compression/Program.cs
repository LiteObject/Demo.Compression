using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace Demo.Compression
{
    /// <summary>
    /// Original Article:
    /// https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-compress-and-extract-files
    /// 
    /// Basic compression and decompression classes:
    /// https://learn.microsoft.com/en-us/dotnet/api/system.io.compression?view=net-7.0
    /// </summary>
    internal class Program
    {
        private const string directoryPath = @".\temp";

        private static async Task Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            /*string testFile = @"C:\Temp\c202205.json";

            byte[] bytes = File.ReadAllBytes(testFile);
            string jsonString = Encoding.UTF8.GetString(bytes);
            // string jsonString = File.ReadAllText(testFile);
            // await WriteToFileAsync(@"C:\Temp\test.json", bytes);

            Console.WriteLine($"\nSource Json Length: {jsonString.Length}");
            Print("\nSource Json:\n" + jsonString[..500]);

            CompressionService.StreamType streamType = CompressionService.StreamType.BrotliStream;
            CompressionLevel compressionLevel = CompressionLevel.Fastest;

            string fileExt = streamType switch
            {
                CompressionService.StreamType.GZipStream => "gz",
                CompressionService.StreamType.BrotliStream => "br",
                _ => throw new NotImplementedException()
            };

            string compressedFileName = $"{streamType}-{compressionLevel}.{fileExt}";

            byte[] compressedJsonStringBytes = await CompressionService.CompressAsync(bytes, streamType, compressionLevel);
            await WriteToFileAsync($"c:\\Temp\\compressed\\{compressedFileName}", compressedJsonStringBytes);

            byte[] decompressedJsonStringBytes = await CompressionService.DecompressAsync(compressedJsonStringBytes, streamType);

            string decompressedJsonString = Encoding.UTF8.GetString(decompressedJsonStringBytes);
            await WriteToFileAsync($"c:\\Temp\\decompressed\\{compressedFileName}.json", decompressedJsonStringBytes);

            Print($"\nDecompressed Json:\n{decompressedJsonString[..500]}");
            Print($"\nDecompressed Json Length: {decompressedJsonString.Length}"); */

            Summary summary = BenchmarkRunner.Run<BenchmarkCompression>();
            Console.WriteLine(summary);

            sw.Stop();
            Console.WriteLine($"\nElapsed Milliseconds: {sw.ElapsedMilliseconds}");
        }

        private static void Print(string value, ConsoleColor consoleTextColor = ConsoleColor.DarkCyan)
        {
            Console.ForegroundColor = consoleTextColor;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        private static async Task WriteToFileAsync(string fileName, byte[] content)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            //using StreamWriter outputFile = new(fileName);
            //await outputFile.WriteAsync(content);

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                Print($"Deleted existing file ({fileName}).", ConsoleColor.DarkYellow);
            }

            await File.WriteAllBytesAsync(fileName, content);
            sw.Stop();
            Print($"\n{nameof(WriteToFileAsync)} took {sw.ElapsedMilliseconds} ms to complete.", ConsoleColor.White);
        }
    }
}