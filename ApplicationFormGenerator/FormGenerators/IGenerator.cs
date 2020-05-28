namespace ApplicationFormGenerator.FormGenerators
{
    interface IGenerator
    {
        string Generate(bool shouldCreatePositiveCases);
    }
}
