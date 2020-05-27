using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationFormGenerator
{
    class BasicTextScanner
    {
        const string Prefix = "__";
        const string PostFix = "__";

        public IEnumerable<Token> ScanForTokens(string text)
        {
            foreach (var word in text.Split())
            {
                if (word.StartsWith(Prefix) && word.EndsWith(PostFix))
                {
                    yield return TokenFactory.Generate(word);
                }
            }
        }
    }
}
