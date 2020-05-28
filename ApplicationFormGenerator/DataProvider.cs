using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApplicationFormGenerator
{
    public static class DataProvider
    {
        public static List<string> Names { get; }
        public static List<string> Surnames { get; }
        public static List<string> Degrees { get; }
        public static List<string> Courses { get; }

        static DataProvider()
        {
            Names = File.ReadAllLines(@"./Data/TokenValues/Names").ToList();
            Surnames = File.ReadAllLines(@"./Data/TokenValues/Surnames").ToList();
            Degrees = File.ReadAllLines(@"./Data/TokenValues/Degrees").ToList();
            Courses = File.ReadAllLines(@"./Data/TokenValues/Courses").ToList();
        }
    }
}
