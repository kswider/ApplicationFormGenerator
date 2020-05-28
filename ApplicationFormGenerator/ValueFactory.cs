using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApplicationFormGenerator
{
    class ValueFactory
    {
        private List<string> _names;
        private List<string> _surnames;
        private List<string> _degrees;
        private List<string> _courses;

        private Dictionary<string, string> _generatedValues = new Dictionary<string, string>();

        private  Random _random = new Random();
       

        public bool ShouldGeneratePositiveTokens { get; set; }

        public  ValueFactory()
        {
            _names = DataProvider.Names;
            _surnames = DataProvider.Surnames;
            _degrees = DataProvider.Degrees;
            _courses = DataProvider.Degrees;
        }

        public string Generate(string stringToken)
        {
            if (CheckIfDecisiveToken(stringToken))
            {
                var positiveAndNegativeToken = stringToken.Split('/');
                stringToken = ShouldGeneratePositiveTokens ? positiveAndNegativeToken[0] : positiveAndNegativeToken[1];
            }

            var isConstToken = CheckIfConstToken(stringToken);
            if (isConstToken && _generatedValues.ContainsKey(stringToken))
                return _generatedValues[stringToken];

            var generatedValue = stringToken switch
            {
                string word when word.Contains("SURNAME") => GenerateSurname(word),
                string word when word.Contains("NAME") => GenerateName(word),                
                string word when word.Contains("NUMBERS") => GenerateNumbers(word),
                string word when word.Contains("NUMBER_RANGE") => GenerateNumberFromRange(word),
                string word when word.Contains("DATE") => GenerateDate(word),
                string word when word.Contains("DEGREE") => GenerateDegree(word),
                string word when word.Contains("COURSE") => GenerateCourse(word),
                _ => "?"
            };

            if (isConstToken)
                _generatedValues[stringToken] = generatedValue;

            return generatedValue;
        }

        private bool CheckIfConstToken(string stringToken) => stringToken.StartsWith("C_");

        private bool CheckIfDecisiveToken(string stringToken) => stringToken.Contains('/');

        private string GenerateName(string stringToken) => _names[_random.Next(_names.Count)];

        private string GenerateSurname(string stringToken) => _surnames[_random.Next(_surnames.Count)];

        private string GenerateNumbers(string stringToken)
        {
            var numbers = "0123456789";
            var regex = new Regex(@"NUMBERS_(?<length>\d+)");
            var length = int.Parse(regex.Match(stringToken).Groups["length"].Value);
            return new string(Enumerable.Repeat(numbers, length).Select(x => x[_random.Next(numbers.Length)]).ToArray());
        }

        private string GenerateDate(string stringToken) => DateTime.Now.AddDays(-_random.Next(30)).ToShortDateString();


        private string GenerateNumberFromRange(string stringToken)
        {
            var regex = new Regex(@"NUMBER_RANGE_(?<lowerBound>\d+)_(?<upperBound>\d+)");
            var match = regex.Match(stringToken);
            var lowerBound = int.Parse(match.Groups["lowerBound"].Value);
            var upperBound = int.Parse(match.Groups["upperBound"].Value);
            return _random.Next(lowerBound, upperBound).ToString();
        }

        private string GenerateDegree(string stringToken) => _degrees[_random.Next(_degrees.Count)];

        private string GenerateCourse(string stringToken) => _courses[_random.Next(_courses.Count)];
    }
}