# CSharp-MadLibs

A basic [Mad Libs](https://en.wikipedia.org/wiki/Mad_Libs) game written in C#.
You pick a story, then type in a few words — a colour, a plural noun, an animal,
a verb, and so on — and the program fills them into a short, often funny, story.
There are three stories to choose from, each with its own set of slots.

It was written as a beginner exercise in C#, so it is a single file with
deliberately simple, heavily-commented code. The stories themselves are defined
as data (a title, a list of slots, and the template lines), so adding a new
story is just adding another entry to the `GetStories()` array — the game loop
does not change.

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
  3 - The Grumpy Dragon
Enter your choice (1-3): 1
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
  3 - The Grumpy Dragon
Enter your choice (1-3): 2
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

Example session (story 3):

```
Welcome to Mad Libs!
Choose a story:
  1 - Roses
  2 - The Hungry Fox
  3 - The Grumpy Dragon
Enter your choice (1-3): 3
Enter an adjective: fierce
Enter a creature: dragon
Enter a place: cave
Enter a food: cheese
Enter an exclamation: ROAR

Long ago, a fierce dragon
lived in the cave.
It was always grumpy about its cheese.
"ROAR!" it roared.
and it grumbled back into the cave.
```

## Input

You are asked for a story number (`1` up to the number of stories currently in
`GetStories()`), then a word for each slot in
that story. The prompts are plain text, so any word works — the funnier the word,
the funnier the story.

## Edge cases

- **End of input** (for example when the program is run from a pipe and the input
  runs out) ends the session with a friendly message instead of printing a broken,
  empty story.
- **Invalid story choice** (anything other than a number from `1` up to the
  number of stories) prints
  `Invalid choice - please enter 1-3.` and asks again, instead of silently
  starting a story. The range in the message is built from the story data, so
  it stays correct when a story is added.
- **Empty word** (a blank or whitespace-only line) is re-asked instead of being
  formatted into the story as a blank slot.
- **Play again** accepts yes case-insensitively and trimmed (`y`, `Y`, `yes`,
  ` y ` all play again); anything else ends the game.

## Tests

The [CI workflow](.github/workflows/ci.yml) compiles the program with `mcs -warn:4`
and runs smoke tests that check both stories fill their slots correctly, that
an end-of-file input ends cleanly rather than producing an empty story, and
that invalid story choices, empty words, and yes-like play-again answers are
handled as described in [Edge cases](#edge-cases).

## License

GPL-3.0 — see the [LICENSE](LICENSE) file.
