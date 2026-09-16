# CSharp-MadLibs

A basic [Mad Libs](https://en.wikipedia.org/wiki/Mad_Libs) game written in C#.
You pick a story, then type in a few words — a colour, a plural noun, an animal,
a verb, and so on — and the program fills them into a short, often funny, story
using `String.Format()`. There are two stories to choose from, each with its own
set of slots.

It was written as a beginner exercise in C#, so it is a single file with
deliberately simple, heavily-commented code.

## Requirements

The project targets **.NET Framework 4.7.2**, but on Linux/Mac it builds and runs
under **Mono**. On Windows you can also open the `.sln` in Visual Studio.

- Linux/Mac: [Mono](https://www.mono-project.com/) (provides `mcs` and `mono`)
- Windows: .NET Framework 4.7.2 (or Visual Studio)

## Building

With Mono, from the repository root:

```sh
mcs -warn:4 -out:MadLib/bin/MadLib.exe MadLib/Program.cs
```

(As with `mcs`, create the output directory first if it does not already exist —
the CI workflow does `mkdir -p MadLib/bin` before compiling.)

## Running

```sh
mono MadLib/bin/MadLib.exe
```

Example session (story 1):

```
Welcome to Mad Libs!
Choose a story:
  1 - Roses
  2 - The Hungry Fox
Enter your choice (1 or 2): 1
Enter a colour: pink
Enter a plural noun: dogs
Enter a thing: ice cream
Enter an adjective: fluffy
Enter an animal: cat
Enter an adverb: gracefully
Enter a noun: tree

Roses are pink
dogs are blue
I love ice cream
Just like fluffy cat
dancing gracefully under the tree
```

Example session (story 2):

```
Welcome to Mad Libs!
Choose a story:
  1 - Roses
  2 - The Hungry Fox
Enter your choice (1 or 2): 2
Enter an adjective: sneaky
Enter an animal: fox
Enter a place: kitchen
Enter a food: chicken
Enter a verb: yelled

Last night, a sneaky fox
sneaked into my kitchen
and ate all of my chicken.
"Get out!" I yelled.
But the fox just yelled
and disappeared into the night.
```

## Input

You are asked for a story number (`1` or `2`), then a word for each slot in that
story. The prompts are plain text, so any word works — the funnier the word, the
funnier the story.

## Edge cases

- **End of input** (for example when the program is run from a pipe and the input
  runs out) ends the session with a friendly message instead of printing a broken,
  empty story.

## Tests

The [CI workflow](.github/workflows/ci.yml) compiles the program with `mcs -warn:4`
and runs smoke tests that check both stories fill their slots correctly and that
an end-of-file input ends cleanly rather than producing an empty story.
