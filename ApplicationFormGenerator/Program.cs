using System;
using System.IO;

namespace ApplicationFormGenerator
{
    class Program
    {
        const int defaultNumberOfDocs = 10;

        static void Main(string[] args)
        {
            var generator = new ECTSDeficitFormGenerator();
            int numberOfDocsToGenerate;
            if (args.Length < 1)
            {
                Console.WriteLine($"Setting number of docs to create to {defaultNumberOfDocs} because parameter wasn't passed");
                numberOfDocsToGenerate = defaultNumberOfDocs;
            }
            else
                numberOfDocsToGenerate = int.Parse(args[0]);

            Directory.CreateDirectory("GeneratedDocs");
            for (int i = 0; i < numberOfDocsToGenerate; i++)
            {
                var text = generator.Generate();
                File.WriteAllText($@"GeneratedDocs/doc_{i}", text);
            }
            Console.WriteLine("Files generated successfully!");
        }
    }
}
