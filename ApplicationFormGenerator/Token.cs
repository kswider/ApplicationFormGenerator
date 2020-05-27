namespace ApplicationFormGenerator
{
    interface IToken
    {
        public string BareRepresentation { get; }
        public string GeneratedValue { get; }
    }
}