using ApplicationFormGenerator.Helpers;
using System;
using System.IO;

namespace ApplicationFormGenerator.FormGenerators
{
    class ECTSDeficitFormGenerator : FormGenerator
    {
        protected override string PathToDataFiles { get; } = @"./Data/ECTSDeficit";
    }
}
