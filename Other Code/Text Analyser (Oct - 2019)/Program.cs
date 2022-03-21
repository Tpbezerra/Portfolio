using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextAnalyzer
{
    class Program
    {
        const string filePath = "C:/Test/english-50mb.txt";

        static int lineCount = 0;
        static int wordCount = 0;
        static int letterCount = 0;
        static int charCount = 0;
        static int theCount = 0;

        static void Main(string[] args)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File doesn't exist!");
                Console.ReadLine();
                return;
            }

            StreamReader sr = new StreamReader(filePath);
            string currentLine = "";

            do
            {
                currentLine = sr.ReadLine();
                string[] fragments = currentLine.Split(' ');

                lineCount++;
                wordCount += fragments.Length;
                charCount += currentLine.Length;

                for (int i = 0; i < currentLine.Length; i++)
                {
                    if (IsLetter(currentLine[i]))
                        letterCount++;
                }
                for (int i = 0; i < fragments.Length; i++)
                {
                    if (IsInteresstingWord(fragments[i]))
                        theCount++;
                }
            } while (!sr.EndOfStream);

            Console.WriteLine("File contains: {0} lines", lineCount);
            Console.WriteLine("File contains: {0} words", wordCount);
            Console.WriteLine("File contains: {0} letters", letterCount);
            Console.WriteLine("File contains: {0} characters", charCount);
            Console.WriteLine("File contains: {0} extras", charCount - letterCount);
            Console.WriteLine("File contains: {0} 'the' words", theCount);

            sr.Close();
            sr.Dispose();

            Console.ReadLine();
        }

        static bool IsLetter(char toTest)
        {
            if ((toTest >= 'a' && toTest <= 'z') || (toTest >= 'A' && toTest <= 'Z'))
                return true;

            return false;
        }

        static bool IsInteresstingWord(string toTest)
        {
            if (toTest.ToLower() == "the")
                return true;

            return false;
        }
    }
}
