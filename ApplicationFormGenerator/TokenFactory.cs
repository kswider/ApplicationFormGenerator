using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApplicationFormGenerator
{
    internal static class TokenFactory
    {
        private static List<string> _names;
        private static List<string> _surnames;
        private static List<string> _degrees;
        private static List<string> _courses;

        private static Random _random = new Random();

        static TokenFactory()
        {
            _names = File.ReadAllLines(@"./Data/TokenValues/Names").ToList();
            _surnames = File.ReadAllLines(@"./Data/TokenValues/Surnames").ToList();
            _degrees = File.ReadAllLines(@"./Data/TokenValues/Degrees").ToList();
            _courses = File.ReadAllLines(@"./Data/TokenValues/Courses").ToList();
        }

        public static Token Generate(string stringToken)
        {
            if (stringToken.Contains("_NAME_"))
                return GenerateNameToken(stringToken);
            else if (stringToken.Contains("_SURNAME_"))
                return GenerateSurnameToken(stringToken);
            else if (stringToken.Contains("_NUMBERS_"))
                return GenerateNumbersToken(stringToken);
            else if (stringToken.Contains("_NUMBER_RANGE"))
                return GenerateNumberFromRangeToken(stringToken);
            else if (stringToken.Contains("_DATE_"))
                return GenerateDateToken(stringToken);
            else if (stringToken.Contains("_DEGREE_"))
                return GenerateDegreeToken(stringToken);
            else if (stringToken.Contains("_COURSE_"))
                return GenerateCourseToken(stringToken);
            return new Token(stringToken, "?");
        }

        private static Token GenerateNameToken(string stringToken)
        {
            var value = _names[_random.Next(_names.Count)];
            return new Token(stringToken, value);
        }

        private static Token GenerateSurnameToken(string stringToken)
        {
            var value = _surnames[_random.Next(_surnames.Count)];
            return new Token(stringToken, value);
        }

        private static Token GenerateNumbersToken(string stringToken)
        {
            var numbers = "0123456789";
            var regex = new Regex(@"NUMBERS_(?<length>\d+)");
            var length = int.Parse(regex.Match(stringToken).Groups["length"].Value);
            var value = new string(Enumerable.Repeat(numbers, length).Select(x => x[_random.Next(numbers.Length)]).ToArray());
            return new Token(stringToken, value);
        }

        private static Token GenerateDateToken(string stringToken)
        {
            var value = DateTime.Now.AddDays(-_random.Next(30)).ToShortDateString();
            return new Token(stringToken, value);
        }

        private static Token GenerateNumberFromRangeToken(string stringToken)
        {
            var regex = new Regex(@"NUMBER_RANGE_(?<lowerBound>\d+)_(?<upperBound>\d+)");
            var match = regex.Match(stringToken);
            var lowerBound = int.Parse(match.Groups["lowerBound"].Value);
            var upperBound = int.Parse(match.Groups["upperBound"].Value);
            var value = _random.Next(lowerBound, upperBound).ToString();
            return new Token(stringToken, value);
        }

        private static Token GenerateDegreeToken(string stringToken)
        {
            var value = _degrees[_random.Next(_degrees.Count)];
            return new Token(stringToken, value);
        }

        private static Token GenerateCourseToken(string stringToken)
        {
            var value = _courses[_random.Next(_courses.Count)];
            return new Token(stringToken, value);
        }
    }
}