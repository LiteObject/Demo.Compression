using BenchmarkDotNet.Attributes;
using System.Text;

namespace Demo.Compression
{
    [RPlotExporter]
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class BenchmarkCompression
    {
        private string? _testJsonSting;
        private byte[] _testBytes;

        [GlobalSetup]
        public void Setup()
        {
            _testJsonSting = "{\r\n  \"Colors\": [\r\n {\r\n   \"numberKey\": 1,\r\n   \"isPrimary\": true,\r\n   \"listColors\": [\"Red\", \"Blue\", \"Yellow\"]\r\n },\r\n\r\n {\r\n   \"numberKey\": 2,\r\n   \"isPrimary\": false,\r\n   \"listColors\": [\"Purple\", \"Green\", \"Orange\"]\r\n } ]\r\n}";;
            _testBytes = Encoding.UTF8.GetBytes(_testJsonSting);
        }

        [Benchmark(Description = "GZip-CompressionLevel-Optimal")]
        public async Task GZipCompressAsync()
        {
            _ = await CompressionService.CompressAsync(_testBytes, CompressionService.StreamType.GZipStream, System.IO.Compression.CompressionLevel.Optimal);
        }

        [Benchmark(Description = "GZip-CompressionLevel-Fastest")]
        public async Task GZipCompressFastestAsync()
        {
            _ = await CompressionService.CompressAsync(_testBytes, CompressionService.StreamType.GZipStream, System.IO.Compression.CompressionLevel.Fastest);
        }

        [Benchmark(Description = "GZip-CompressionLevel-SmallestSize")]
        public async Task GZipCompressSmallestSizeAsync()
        {
            _ = await CompressionService.CompressAsync(_testBytes, CompressionService.StreamType.GZipStream, System.IO.Compression.CompressionLevel.SmallestSize);
        }

        [Benchmark(Description = "Brotli-CompressionLevel-Optimal")]
        public async Task BrotliCompressAsync()
        {
            _ = await CompressionService.CompressAsync(_testBytes, CompressionService.StreamType.BrotliStream, System.IO.Compression.CompressionLevel.Optimal);
        }

        [Benchmark(Description = "Brotli-CompressionLevel-Fastest")]
        public async Task BrotliCompressFastestAsync()
        {
            _ = await CompressionService.CompressAsync(_testBytes, CompressionService.StreamType.BrotliStream, System.IO.Compression.CompressionLevel.Fastest);
        }

        [Benchmark(Description = "Brotli-CompressionLevel-SmallestSize")]
        public async Task BrotliCompressSmallestSizeAsync()
        {
            _ = await CompressionService.CompressAsync(_testBytes, CompressionService.StreamType.BrotliStream, System.IO.Compression.CompressionLevel.SmallestSize);
        }
    }
}
