# Demo Compression
* Simple implementation of GZip & Brotli

---
|                               Method |       Mean |     Error |    StdDev |     Median | Rank |   Gen0 | Allocated |
|------------------------------------- |-----------:|----------:|----------:|-----------:|-----:|-------:|----------:|
|      Brotli-CompressionLevel-Fastest |   6.501 us | 0.0286 us | 0.0253 us |   6.501 us |    1 | 0.1144 |     728 B |
|        GZip-CompressionLevel-Optimal |  14.644 us | 0.1857 us | 0.2664 us |  14.595 us |    2 | 0.1221 |     824 B |
|        GZip-CompressionLevel-Fastest |  15.963 us | 0.6019 us | 1.7748 us |  17.033 us |    3 | 0.1221 |     832 B |
|   GZip-CompressionLevel-SmallestSize |  16.739 us | 0.7599 us | 2.2406 us |  17.918 us |    4 | 0.1221 |     824 B |
|      Brotli-CompressionLevel-Optimal |  31.590 us | 0.1753 us | 0.1640 us |  31.554 us |    5 | 0.0610 |     704 B |
| Brotli-CompressionLevel-SmallestSize | 884.031 us | 4.0152 us | 3.5594 us | 884.119 us |    6 |      - |     690 B |

---
* [How to: Compress and extract files](https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-compress-and-extract-files)
* [Basic compression and decompression classes](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression?view=net-7.0)