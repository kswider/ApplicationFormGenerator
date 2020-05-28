using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationFormGenerator.FormGenerators
{
    static class GeneratorFactory
    {
        public static IGenerator CreateGenerator(DocumentType type)
        {
            return type switch
            {
                DocumentType.Any => new AnyFormGenerator(),
                DocumentType.ECTSDeficit => new ECTSDeficitFormGenerator(),
                _ => throw new Exception("This document type is not supported!")
            };
        }
    }
}
