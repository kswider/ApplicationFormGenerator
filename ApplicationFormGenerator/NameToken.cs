using System;
using System.IO;

namespace ApplicationFormGenerator
{
    class Token
    {
        public string BareRepresentation { get; }
        public string GeneratedValue { get; }

        public Token(string bareRepresentation, string generatedValue)
        {
            BareRepresentation = bareRepresentation;
            GeneratedValue = generatedValue;
        }
    }
}
