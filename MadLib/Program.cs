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
        // The differences from a plain Console.ReadLine() are:
        //   * A null result means there is no more input to read (end of
        //     file), which happens when the program is run from a pipe and the
        //     input runs out. Instead of formatting an empty story, it says
        //     goodbye and stops, so the user always gets a message.
        //   * An empty or whitespace-only line is not a word, so it re-asks
        //     with the same prompt instead of formatting a broken story line
        //     like "Roses are" with nothing after it (issue #11).
        static string ReadWord(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string word = Console.ReadLine();
                if (word == null)                                   // No more input (end of file), e.g. running from a pipe.
                {
                    Console.WriteLine("No more input - goodbye.");   // Friendly message instead of an empty story.
                    Environment.Exit(0);                             // End the program cleanly.
                }
                if (word.Trim().Length > 0)                          // A real word - hand it back.
                {
                    return word;
                }
                Console.WriteLine("Please enter a word - it can't be empty."); // Blank line: ask again.
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                // Offer two stories and let the player pick one.
                Console.WriteLine("Welcome to Mad Libs!");
                Console.WriteLine("Choose a story:");
                Console.WriteLine("  1 - Roses");
                Console.WriteLine("  2 - The Hungry Fox");
                string choice = ReadWord("Enter your choice (1 or 2): ");

                // Only "1" and "2" start a story; anything else gets an error
                // and the prompt again, so a typo can't start the wrong story
                // silently (issue #9).
                string story = null;
                while (story == null)
                {
                    string trimmedChoice = choice.Trim();
                    if (trimmedChoice == "1" || trimmedChoice == "2")
                    {
                        story = trimmedChoice;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice - please enter 1 or 2.");
                        choice = ReadWord("Enter your choice (1 or 2): ");
                    }
                }

                if (story == "2")
                {
                    // Story 2: The Hungry Fox. Five words, with the animal and verb used twice.
                    string adjective = ReadWord("Enter an adjective: ");
                    string animal    = ReadWord("Enter an animal: ");
                    string place     = ReadWord("Enter a place: ");
                    string food      = ReadWord("Enter a food: ");
                    string verb      = ReadWord("Enter a verb: ");

                    Console.WriteLine();
                    Console.WriteLine(String.Format("Last night, a {0} {1}", adjective, animal));   // Use of String.Format() to put the words into the story.
                    Console.WriteLine(String.Format("sneaked into my {0}", place));
                    Console.WriteLine(String.Format("and ate all of my {0}.", food));
                    Console.WriteLine(String.Format("\"Get out!\" I {0}.", verb));
                    Console.WriteLine(String.Format("But the {0} just {1}", animal, verb));         // The same words can be used more than once.
                    Console.WriteLine("and disappeared into the night.");
                }
                else
                {
                    // Story 1: Roses. The original story, with a few more slots added.
                    string colour     = ReadWord("Enter a colour: ");
                    string pluralNoun = ReadWord("Enter a plural noun: ");
                    string thing      = ReadWord("Enter a thing: ");
                    string adjective  = ReadWord("Enter an adjective: ");
                    string animal     = ReadWord("Enter an animal: ");
                    string adverb     = ReadWord("Enter an adverb: ");
                    string noun       = ReadWord("Enter a noun: ");

                    Console.WriteLine();
                    Console.WriteLine(String.Format("Roses are {0}", colour));          // Use of String.Format() to put the words into the story.
                    Console.WriteLine(String.Format("{0} are blue", pluralNoun));
                    Console.WriteLine(String.Format("I love {0}", thing));
                    Console.WriteLine(String.Format("Just like {0} {1}", adjective, animal));
                    Console.WriteLine(String.Format("dancing {0} under the {1}", adverb, noun));
                }

                Console.WriteLine();
                string playAgain = ReadWord("Play again? (y/n): ");
                if (playAgain != "y")
                {
                    break;
                }
                Console.WriteLine();
            }
        }
    }
}
