using System;

namespace TextProcessingApp
{
    public class CharCounter
    {
        public int CountCharacterOccurrences(string text, char character) //1
        {                                                                 //2
            if (text == null)                                             //3
            {                                                             //4
                throw new ArgumentNullException(nameof(text), "Text cannot be null"); //5
            }                                                             //6
            int count = 0;                                                //7
            int index = 0;                                                //8
            int length = text.Length;                                     //9
            while (index < length)                                        //10
            {                                                             //11
                if (text[index] == character)                             //12
                {                                                         //13
                    count++;                                              //14
                }                                                         //15
                index++;                                                  //16
            }                                                             //17
            return count;                                                 //18
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            RunDemo();
        }

        static void RunDemo()
        {
            CharCounter counter = new CharCounter();

            string[] testStrings = {
                "apple", "banana", "hello world", "programming", ""
            };
            char[] testChars = { 'a', 'n', 'l', 'm', 'x' };

            foreach (var str in testStrings)
            {
                foreach (var ch in testChars)
                {
                    try
                    {
                        int count = counter.CountCharacterOccurrences(str, ch);
                        Console.WriteLine($"Символ '{ch}' в '{str}': {count}");
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Ошибка: строка не может быть null");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}