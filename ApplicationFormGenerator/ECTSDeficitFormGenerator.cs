using ApplicationFormGenerator.Helpers;
using System;
using System.IO;

namespace ApplicationFormGenerator
{
    class ECTSDeficitFormGenerator : IGenerator
    {
        const string pathToDataFiles = @"./Data/ECTSDeficit";

        public string Generate(bool shouldCreatePositiveCases)
        {
            var files = Directory.GetFiles(pathToDataFiles);
            var random = new Random();
            var randomFilePath = files[random.Next(files.Length)];
            var text =  File.ReadAllText(randomFilePath);

            var basicTextScanner = new BasicTextScanner(shouldCreatePositiveCases);
            var tokens = basicTextScanner.ScanAndGenerateTokens(text);
            foreach (var token in tokens)
                text = text.ReplaceFirst(token.BareRepresentation, token.Value);
            return text;
        }
    }
}
