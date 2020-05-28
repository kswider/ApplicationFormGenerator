using ApplicationFormGenerator.FormGenerators;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ApplicationFormGenerator
{
    class Program
    {
        const string OutputDirectory = "GeneratedDocs";

        static void Main(string[] args)
        {
            Console.WriteLine($"Provide number of documents to generate:");
            var numberOfDocsToGenerate = int.Parse(Console.ReadLine());

            Console.WriteLine($"Provide type of documents to generate:");
            Console.WriteLine($"0 - Any Document, 1 - ECTS deficit");
            var documentType = (DocumentType)int.Parse(Console.ReadLine());

            Console.WriteLine($"Provide staus of generated docs: 1 - Positive Cases, 2 - Negative Cases");
            bool shouldCreatePositiveCases = int.Parse(Console.ReadLine()) == 1;

            var generator = GeneratorFactory.CreateGenerator(documentType);
            if (Directory.Exists(OutputDirectory))
                Directory.Delete(OutputDirectory, true);

            Directory.CreateDirectory(OutputDirectory);
            Parallel.For(0, numberOfDocsToGenerate, i =>
            {
                var text = generator.Generate(shouldCreatePositiveCases);
                File.WriteAllText($@"{OutputDirectory}/doc_{i}", text);
            });
            Console.WriteLine("Files generated successfully!");
        }
    }
}
