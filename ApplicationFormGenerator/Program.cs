using System;
using System.IO;

namespace ApplicationFormGenerator
{
    class Program
    {
        const int defaultNumberOfDocs = 10;

        static void Main(string[] args)
        {
            Console.WriteLine($"Provide number of documents to generate:");
            var numberOfDocsToGenerate = int.Parse(Console.ReadLine());

            Console.WriteLine($"Provide type of documents to generate:");
            Console.WriteLine($"1 - ECTS deficit");
            var documentType = (DocumentType)int.Parse(Console.ReadLine());

            Console.WriteLine($"Provide staus of generated docs: 1 - Positive Cases, 2 - Negative Cases");
            bool shouldCreatePositiveCases = int.Parse(Console.ReadLine()) == 1;

            var generator = GeneratorFactory.CreateGenerator(documentType);
            Directory.Delete("GeneratedDocs", true);
            Directory.CreateDirectory("GeneratedDocs");
            for (int i = 0; i < numberOfDocsToGenerate; i++)
            {
                var text = generator.Generate(shouldCreatePositiveCases);
                File.WriteAllText($@"GeneratedDocs/doc_{i}", text);
            }
            Console.WriteLine("Files generated successfully!");
        }
    }
}
