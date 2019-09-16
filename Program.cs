using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    class Program
    {
        private const string Address = "";//write the url of file you want
        static void Main(string[] args)
        {
             string contents;
             using (var wc = new System.Net.WebClient())
             {
                 contents = wc.DownloadString(Address);
             }

            string[] lines = File.ReadAllLines(contents);
            int[] CapLetters = new int[26]; // there could be a total 26 different letters that are capatalized
            int[] letters = new int[26]; // there could be 26 different letter
            int TotalCapitalizedLetters = 0; //count for letters that are capitalized
            Dictionary<string, int> word = new Dictionary<string, int>(); // To get most common word
            int startIndex = 0, lastIndex = 0;
            foreach (string currentRow in lines) // Read each line
            {
                char[] charArray = currentRow.ToCharArray(); // Read each char
                foreach (char currentChar in charArray)
                {
                    //GET COUNT as per current letter is small or capital
                    if (currentChar >= 'a' && currentChar <= 'z')
                    {
                        letters[(int)currentChar - 97]++; // Decide small letter count

                    }
                    else
                    {
                        CapLetters[(int)currentChar - 65]++; // get cap letter count
                        TotalCapitalizedLetters++; // Total capital letter count
                        string newString = currentRow.Substring(startIndex, lastIndex - startIndex); //Read each word
                        if (lastIndex != 0)
                        {
                            if (word.ContainsKey(newString)) word[newString] += 1; // Insert into dictionary to get distinct count
                            else word.Add(newString, 1);
                            startIndex = lastIndex;
                        }

                    }
                    lastIndex++;

                }
                if (lastIndex != startIndex) // If still words are there, add to dictionary
                {
                    string s = currentRow.Substring(startIndex, lastIndex - startIndex);
                    if (word.ContainsKey(s))
                    {
                        word[s] += 1;
                    }
                    else
                    {
                        word.Add(s, 1);
                    }
                }
                //string lastString = currentRow.Substring(startIndex, lastIndex);
                // cd.Add(lastString, cd.ContainsKey(lastString) ? 1 + cd[lastString] : 1);
            }

            // Show the results
            Console.WriteLine("\nAll count of each small letter from file is ");
            for (int i = 0; i < 26; i++) // Show each letter count
            {
                Console.Write((char)(97 + i) + " -- " + letters[i] + " ");
            }

            Console.WriteLine("\nAll count of each capital letter from file is ");
            for (int i = 0; i < 26; i++)
            {
                Console.Write((char)(65 + i) + " -- " + CapLetters[i] + " ");

            }
            Console.WriteLine("Total capitalized letters: " + TotalCapitalizedLetters);
            Console.WriteLine("Most common words and there count: ");
            foreach (KeyValuePair<string, int> kvp in word)
            {
                if (kvp.Value > 1)
                {
                    Console.WriteLine(kvp.Key + "---" + kvp.Value + " times");
                }
            }

            Console.WriteLine("The most common 2 character prefix and the number of occurrences in the text file are follwing");
            foreach (KeyValuePair<string, int> kvp in word)
            {
                if (kvp.Value > 1)
                {
                    Console.WriteLine(kvp.Key.Substring(0, 2) + " --- " + kvp.Value + " times");
                }
            }
            Console.ReadKey();
        }
    }
}
