using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadLib
{
    internal class Program
    {
        // Reads one word of input for the game.
        //
        // The only difference from a plain Console.ReadLine() is that it checks
        // for a null result. A null means there is no more input to read (end of
        // file), which happens when the program is run from a pipe and the input
        // runs out. Instead of formatting an empty story, it says goodbye and
        // stops, so the user always gets a message.
        static string ReadWord(string prompt)
        {
            Console.WriteLine(prompt);
            string word = Console.ReadLine();
            if (word == null)                                   // No more input (end of file), e.g. running from a pipe.
            {
                Console.WriteLine("No more input - goodbye.");   // Friendly message instead of an empty story.
                Environment.Exit(0);                             // End the program cleanly.
            }
            return word;                                        // Store and hand back the word the user typed.
        }

        static void Main(string[] args)
        {
            string colour, pluralNoun, thing;                               // Define "colour", pluralNoun, and thing as empty string variables.

            colour = ReadWord("Enter a colour: ");                          // Read the user's input, store it in the string variable "colour".

            pluralNoun = ReadWord("Enter a plural noun: ");                 // Same process, but for the plural noun.

            thing = ReadWord("Enter a thing: ");                            // Same process, but for the thing.

            Console.WriteLine(String.Format("Roses are {0}", colour));      // Use of String.Format() to input the string variable "colour" into a Console.WriteLine string. 
            Console.WriteLine(String.Format("{0} are blue", pluralNoun));
            Console.WriteLine(String.Format("I love {0}", thing));

            Console.ReadLine();                                             // Prevents the program from closing on its own.
        }
    }
}
