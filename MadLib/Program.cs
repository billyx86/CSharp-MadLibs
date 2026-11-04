using System;
using System.Text.RegularExpressions;

namespace MadLib
{
    internal class Program
    {
        // A single blank in a story: the prompt to ask the player and the
        // name of the word it collects. The name is written into the story
        // template as {name}, and the same name may be used on several lines
        // (e.g. the animal and verb in "The Hungry Fox" each appear twice).
        internal class Slot
        {
            public string Prompt;   // what to ask for
            public string Name;     // placeholder token used in the template, e.g. "adjective"
            public string Word;     // the word the player entered (filled in while playing)

            public Slot(string prompt, string name)
            {
                Prompt = prompt;
                Name = name;
            }
        }

        // A story is now pure data (issue #17): a title, the slots to fill,
        // and the template lines that turn those words into the finished
        // story. Each template line uses {slot name} placeholders; printing
        // the story just swaps every {name} for the word the player typed.
        internal class Story
        {
            public string Title;
            public Slot[] Slots;
            public string[] Lines;

            public Story(string title, Slot[] slots, string[] lines)
            {
                Title = title;
                Slots = slots;
                Lines = lines;
            }

            // Ask for every slot's word, then print the finished story.
            public void Play()
            {
                for (int i = 0; i < Slots.Length; i++)
                {
                    Slots[i].Word = ReadWord(Slots[i].Prompt);
                }

                Console.WriteLine();                       // Blank line before the story.
                for (int i = 0; i < Lines.Length; i++)
                {
                    Console.WriteLine(Fill(Lines[i]));     // Swap each {name} for its word.
                }
            }

            // Replace every {slot name} in a line with that slot's word.
            //
            // The replacement runs in a single pass over the template, so a
            // word the player typed is never scanned again. Sequential
            // String.Replace calls did rescan the already-filled text, and a
            // word that contained another slot's token was rewritten by the
            // later replacement (issue #23).
            private string Fill(string line)
            {
                return Regex.Replace(line, "\\{([A-Za-z]+)\\}", delegate(Match match)
                {
                    string name = match.Groups[1].Value;
                    for (int i = 0; i < Slots.Length; i++)
                    {
                        if (Slots[i].Name == name)
                        {
                            return Slots[i].Word;
                        }
                    }
                    return match.Value;   // Not a slot of this story: keep it verbatim.
                });
            }
        }

        // The stories, as data (issue #16, #17). Adding a story is now just
        // adding another entry to this array - no changes to the game loop.
        static Story[] GetStories()
        {
            return new Story[]
            {
                new Story(
                    "Roses",
                    new Slot[]
                    {
                        new Slot("Enter a colour: ", "colour"),
                        new Slot("Enter a plural noun: ", "pluralNoun"),
                        new Slot("Enter a thing: ", "thing"),
                        new Slot("Enter an adjective: ", "adjective"),
                        new Slot("Enter an animal: ", "animal"),
                        new Slot("Enter an adverb: ", "adverb"),
                        new Slot("Enter a noun: ", "noun"),
                    },
                    new string[]
                    {
                        "Roses are {colour}",
                        "{pluralNoun} are blue",
                        "I love {thing}",
                        "Just like {adjective} {animal}",
                        "dancing {adverb} under the {noun}",
                    }),

                new Story(
                    "The Hungry Fox",
                    new Slot[]
                    {
                        new Slot("Enter an adjective: ", "adjective"),
                        new Slot("Enter an animal: ", "animal"),
                        new Slot("Enter a place: ", "place"),
                        new Slot("Enter a food: ", "food"),
                        new Slot("Enter a verb: ", "verb"),
                    },
                    new string[]
                    {
                        "Last night, a {adjective} {animal}",
                        "sneaked into my {place}",
                        "and ate all of my {food}.",
                        "\"Get out!\" I {verb}.",
                        "But the {animal} just {verb}",   // The same word can be used more than once.
                        "and disappeared into the night.",
                    }),

                new Story(
                    "The Grumpy Dragon",
                    new Slot[]
                    {
                        new Slot("Enter an adjective: ", "adjective"),
                        new Slot("Enter a creature: ", "creature"),
                        new Slot("Enter a place: ", "place"),
                        new Slot("Enter a food: ", "food"),
                        new Slot("Enter an exclamation: ", "exclamation"),
                    },
                    new string[]
                    {
                        "Long ago, a {adjective} {creature}",
                        "lived in the {place}.",
                        "It was always grumpy about its {food}.",
                        "\"{exclamation}!\" it roared.",
                        "and it grumbled back into the {place}.",   // The same word can be used more than once.
                    }),
            };
        }

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
                    return word.Trim();                               // Trimmed, so leading/trailing whitespace never reaches the story (issue #13).
                }
                Console.WriteLine("Please enter a word - it can't be empty."); // Blank line: ask again.
            }
        }

        // Accepts the play-again answer. Anything yes-like (trimmed,
        // case-insensitive, so "Y", " y " and "yes" all count) plays again;
        // everything else ends the game (issue #10).
        static bool IsYes(string answer)
        {
            string trimmed = answer.Trim().ToLowerInvariant();
            return trimmed == "y" || trimmed == "yes";
        }

        static void Main(string[] args)
        {
            Story[] stories = GetStories();
            while (true)
            {
                // Offer the stories and let the player pick one. The menu is
                // built from the data, so it grows automatically when a story
                // is added (issue #16).
                Console.WriteLine("Welcome to Mad Libs!");
                Console.WriteLine("Choose a story:");
                for (int i = 0; i < stories.Length; i++)
                {
                    Console.WriteLine("  " + (i + 1) + " - " + stories[i].Title);
                }
                string choice = ReadWord("Enter your choice (1-" + stories.Length + "): ");

                // Only a number that points at a real story starts one; anything
                // else gets an error and the prompt again, so a typo can't start
                // the wrong story silently (issue #9).
                Story chosen = null;
                while (chosen == null)
                {
                    string trimmedChoice = choice.Trim();
                    int number;
                    if (int.TryParse(trimmedChoice, out number)
                        && number >= 1
                        && number <= stories.Length)
                    {
                        chosen = stories[number - 1];
                    }
                    else
                    {
                        // Built from the data like the menu prompt, so the
                        // range stays correct when a story is added (issue #19).
                        Console.WriteLine("Invalid choice - please enter 1-" + stories.Length + ".");
                        choice = ReadWord("Enter your choice (1-" + stories.Length + "): ");
                    }
                }

                chosen.Play();

                Console.WriteLine();
                string playAgain = ReadWord("Play again? (y/n): ");
                if (!IsYes(playAgain))
                {
                    break;
                }
                Console.WriteLine();
            }
        }
    }
}
