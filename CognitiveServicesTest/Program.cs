using CognitiveServicesTest.LanguageUnderstanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServicesTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MyLuisClient client = new MyLuisClient("a9777fd2-0c56-4a76-b3b4-740b387c05d5", "0c13af8b1228447bb2ce26e7be709940");
            client.Execute();
            Console.ReadKey();
        }
    }
}