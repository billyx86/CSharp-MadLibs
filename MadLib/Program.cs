using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadLib
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string colour, pluralNoun, thing;                               // Define "colour", pluralNoun, and thing as empty string variables.

            Console.WriteLine("Enter a colour: ");
            colour = Console.ReadLine();                                    // Read the user's input, store it in the string variable "colour".

            Console.WriteLine("Enter a plural noun: ");
            pluralNoun = Console.ReadLine();

            Console.WriteLine("Enter a thing: ");
            thing = Console.ReadLine();

            Console.WriteLine(String.Format("Roses are {0}", colour));      // Use of String.Format() to input the string variable "colour" into a Console.WriteLine string. 
            Console.WriteLine(String.Format("{0} are blue", pluralNoun));
            Console.WriteLine(String.Format("I love {0}", thing));

            Console.ReadLine();                                             // Prevents the program from closing on its own.
        }
    }
}
