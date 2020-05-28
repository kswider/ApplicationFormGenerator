﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApplicationFormGenerator
{
    internal class ValueFactory
    {
        private static List<string> _names;
        private static List<string> _surnames;
        private static List<string> _degrees;
        private static List<string> _courses;

        private static Random _random = new Random();
        
        private static ValueFactory _instance;
        public static ValueFactory Instatnce
        {
            get
            {
                if (_instance == null)
                    _instance = new ValueFactory();
                return _instance;
            }
        }

        public bool ShouldGeneratePositiveTokens { get; set; }

        private ValueFactory()
        {
            _names = File.ReadAllLines(@"./Data/TokenValues/Names").ToList();
            _surnames = File.ReadAllLines(@"./Data/TokenValues/Surnames").ToList();
            _degrees = File.ReadAllLines(@"./Data/TokenValues/Degrees").ToList();
            _courses = File.ReadAllLines(@"./Data/TokenValues/Courses").ToList();
        }

        public string Generate(string stringToken)
        {
            if (stringToken.Contains('/'))
            {
                var positiveAndNegativeToken = stringToken.Split('/');
                stringToken = ShouldGeneratePositiveTokens ? positiveAndNegativeToken[0] : positiveAndNegativeToken[1];
            }

            return stringToken switch
            {
                string word when word.Equals("NAME") => GenerateName(word),
                string word when word.Equals("SURNAME") => GenerateSurname(word),
                string word when word.Contains("NUMBERS") => GenerateNumbers(word),
                string word when word.Contains("NUMBER_RANGE") => GenerateNumberFromRange(word),
                string word when word.Contains("DATE") => GenerateDate(word),
                string word when word.Contains("DEGREE") => GenerateDegree(word),
                string word when word.Contains("COURSE") => GenerateCourse(word),
                _ => "?"
            };
        }

        private string GenerateName(string stringToken)
            => _names[_random.Next(_names.Count)];


        private string GenerateSurname(string stringToken)
            => _surnames[_random.Next(_surnames.Count)];


        private string GenerateNumbers(string stringToken)
        {
            var numbers = "0123456789";
            var regex = new Regex(@"NUMBERS_(?<length>\d+)");
            var length = int.Parse(regex.Match(stringToken).Groups["length"].Value);
            return new string(Enumerable.Repeat(numbers, length).Select(x => x[_random.Next(numbers.Length)]).ToArray());
        }

        private string GenerateDate(string stringToken)
            => DateTime.Now.AddDays(-_random.Next(30)).ToShortDateString();


        private string GenerateNumberFromRange(string stringToken)
        {
            var regex = new Regex(@"NUMBER_RANGE_(?<lowerBound>\d+)_(?<upperBound>\d+)");
            var match = regex.Match(stringToken);
            var lowerBound = int.Parse(match.Groups["lowerBound"].Value);
            var upperBound = int.Parse(match.Groups["upperBound"].Value);
            return _random.Next(lowerBound, upperBound).ToString();
        }

        private string GenerateDegree(string stringToken)
            => _degrees[_random.Next(_degrees.Count)];

        private string GenerateCourse(string stringToken)
            => _courses[_random.Next(_courses.Count)];
    }
}