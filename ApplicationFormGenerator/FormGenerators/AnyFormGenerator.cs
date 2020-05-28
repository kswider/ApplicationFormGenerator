using ApplicationFormGenerator.Helpers;
using System;
using System.IO;

namespace ApplicationFormGenerator.FormGenerators
{
    class AnyFormGenerator : FormGenerator
    {
        protected override string PathToDataFiles { get; } = @"./Data/Any";
    }
}
