using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationFormGenerator
{
    class BasicTextScanner
    {
        const string Prefix = "__";
        const string PostFix = "__";

        bool _shouldCreatePositiveCases;

        public BasicTextScanner(bool shouldCreatePositiveCases)
        {
            _shouldCreatePositiveCases = shouldCreatePositiveCases;
        }

        public IEnumerable<Token> ScanAndGenerateTokens(string text)
        {
            foreach (var word in text.Split())
            {
                if (word.StartsWith(Prefix) && word.EndsWith(PostFix))
                {
                    var textToken = word[2..^2];
                    var valueFactory = new ValueFactory();
                    valueFactory.ShouldGeneratePositiveTokens = _shouldCreatePositiveCases;
                    var value = valueFactory.Generate(textToken);
                    yield return new Token(word, value);
                }
            }
        }
    }
}
