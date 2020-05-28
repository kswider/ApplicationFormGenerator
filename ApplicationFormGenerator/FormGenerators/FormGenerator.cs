using ApplicationFormGenerator.Helpers;
using System;
using System.IO;
using System.Linq;

namespace ApplicationFormGenerator.FormGenerators
{
    abstract class FormGenerator : IGenerator
    {
        private Random _random = new Random();

        protected abstract string PathToDataFiles { get; }

        public virtual string Generate(bool shouldCreatePositiveCases)
        {
            var files = Directory.GetFiles(PathToDataFiles);
            if (!files.Any())
                throw new Exception($"There are no files in {PathToDataFiles} path! Provide some schemas before running generator!");

            var randomFilePath = files[_random.Next(files.Length)];
            var text = File.ReadAllText(randomFilePath);

            var basicTextScanner = new BasicTextScanner(shouldCreatePositiveCases);
            var tokens = basicTextScanner.ScanAndGenerateTokens(text);
            foreach (var token in tokens)
                text = text.ReplaceFirst(token.BareRepresentation, token.Value);
            return text;
        }
    }
}