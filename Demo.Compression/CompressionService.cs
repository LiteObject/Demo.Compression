using System.Diagnostics;
using System.IO.Compression;
using System.Text;

namespace Demo.Compression
{
    public class CompressionService
    {
        public enum StreamType
        {
            GZipStream,
            BrotliStream
        }

        public static async Task<byte[]> CompressAsync(byte[] inputBytes, StreamType streamType = StreamType.GZipStream, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            ArgumentNullException.ThrowIfNull(nameof(inputBytes));

            using MemoryStream outputStream = new();

            using Stream compressorStream = streamType switch
            {
                StreamType.GZipStream => new GZipStream(outputStream, compressionLevel),
                StreamType.BrotliStream => new BrotliStream(outputStream, compressionLevel),
                // _ => new GZipStream(memoryStream, compressionLevel)
                _ => throw new NotImplementedException(),
            };

            // await compressorStream.WriteAsync(inputBytes, 0, inputBytes.Length);
            await compressorStream.WriteAsync(inputBytes);
            compressorStream.Close();

            return outputStream.ToArray();
        }

        public static async Task<byte[]> DecompressAsync(byte[] compressedBytes, StreamType streamType = StreamType.GZipStream)
        {
            ArgumentNullException.ThrowIfNull(nameof(compressedBytes));

            using MemoryStream inputStream = new(compressedBytes);
            // inputStream.Position = 0;
            using MemoryStream outputStream = new();

            using Stream decompressorStream = streamType switch
            {
                StreamType.GZipStream => new GZipStream(inputStream, CompressionMode.Decompress, leaveOpen: false),
                StreamType.BrotliStream => new BrotliStream(inputStream, CompressionMode.Decompress, leaveOpen: false),
                _ => throw new NotImplementedException(),
            };

            await decompressorStream.CopyToAsync(outputStream);

            return outputStream.ToArray();
        }

        public async Task<byte[]> CompressStringAsync(string jsonString, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            using MemoryStream memoryStream = new();

            using (GZipStream stream = new(memoryStream, compressionLevel))
            {
                byte[] jsonStringByteArray = Encoding.UTF8.GetBytes(jsonString);
                await stream.WriteAsync(jsonStringByteArray, 0, jsonStringByteArray.Length);
            }

            stopwatch.Stop();
            Console.WriteLine($"{nameof(CompressStringAsync)} took {stopwatch.ElapsedMilliseconds} ms to complete.");

            return memoryStream.ToArray();
        }

        public async Task<string> DecompressStringAsync(byte[] bytes)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            using MemoryStream memoryStream = new(bytes);
            using MemoryStream outputStream = new();
            using (GZipStream decompressStream = new(memoryStream, CompressionMode.Decompress))
            {
                await decompressStream.CopyToAsync(outputStream);
            }

            stopwatch.Stop();
            Console.WriteLine($"{nameof(DecompressStringAsync)} took {stopwatch.ElapsedMilliseconds} ms to complete.");

            return Encoding.UTF8.GetString(outputStream.ToArray());
        }

        public static void CompressFolder(string directoryPath, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(directoryPath, nameof(directoryPath));

            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException();
            }

            DirectoryInfo directorySelected = new(directoryPath);

            foreach (FileInfo fileToCompress in directorySelected.GetFiles())
            {
                using FileStream originalFileStream = fileToCompress.OpenRead();

                if ((File.GetAttributes(fileToCompress.FullName) &
                   FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                {
                    using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                    {
                        using GZipStream compressionStream = new(compressedFileStream, compressionLevel);
                        originalFileStream.CopyTo(compressionStream);
                    }

                    FileInfo info = new(directoryPath + Path.DirectorySeparatorChar + fileToCompress.Name + ".gz");
                    Console.WriteLine($"Compressed {fileToCompress.Name} from {fileToCompress.Length} to {info.Length} bytes.");
                }
            }
        }

    }
}
