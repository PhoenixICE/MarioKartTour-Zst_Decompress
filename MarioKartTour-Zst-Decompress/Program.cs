using System;
using System.IO;
using ZstdNet;

namespace MarioKartTour_Zst_Decompress
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zst File decompressor for Mario Kart Tour - IcyPhoenix | Special thanks to tryso, kuronosuFear");

            var dict = File.ReadAllBytes("1.0.1.zdict");

            Directory.CreateDirectory(@".\zst\");
            Directory.CreateDirectory(@".\dzst\");

            var files = Directory.GetFiles(@".\zst\", "*.zst");

            if (files.Length == 0)
            {
                Console.WriteLine("No files found - Place zst files in the folder named zst.");
                Console.ReadKey();
                return;
            }

            using (var options = new DecompressionOptions(dict))
            using (var decompressor = new Decompressor(options))
            {
                foreach (var file in files)
                {
                    try
                    {
                        var fileBytes = File.ReadAllBytes(file);
                        var decompressedFile = decompressor.Unwrap(fileBytes);
                        File.WriteAllBytes(@".\dzst\" + Path.GetFileNameWithoutExtension(file), decompressedFile);
                        Console.WriteLine($"File successfully decompressed {file}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Could not decompress {file} - {e.ToString()}!");
                    }
                }
            }

            Console.WriteLine("Completed. Please find all decompressed files in the folder dzst.");
            Console.ReadKey();
        }
    }
}
