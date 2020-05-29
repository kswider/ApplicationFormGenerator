using ApplicationFormGenerator.FormGenerators;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text.RegularExpressions;
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

            Console.WriteLine($"SignleFileOutput: 1 - Yes, 2 - No");
            bool singleFileOutput = int.Parse(Console.ReadLine()) == 1;

            var generator = GeneratorFactory.CreateGenerator(documentType);
            if (Directory.Exists(OutputDirectory))
                Directory.Delete(OutputDirectory, true);

            Directory.CreateDirectory(OutputDirectory);
            if (!singleFileOutput)
            {
                Parallel.For(0, numberOfDocsToGenerate, i =>
                {
                    var text = generator.Generate(shouldCreatePositiveCases);
                    File.WriteAllText($@"{OutputDirectory}/doc_{i}", text);
                });
            }
            else
            {
                ConcurrentBag<string> texts = new ConcurrentBag<string>();
                Parallel.For(0, numberOfDocsToGenerate, i =>
                {
                    var text = generator.Generate(shouldCreatePositiveCases);
                    texts.Add(text);
                });
                using (var sw = new StreamWriter($@"{OutputDirectory}/output.csv"))
                {
                    sw.WriteLine("Result;Text");
                    foreach (var text in texts)
                    {
                        var textSpacesSeparated = Regex.Replace(text, @"\s+", " ");
                        sw.WriteLine($"{shouldCreatePositiveCases};{textSpacesSeparated}");
                    }
                }
            }
            Console.WriteLine("Files generated successfully!");
        }
    }
}
