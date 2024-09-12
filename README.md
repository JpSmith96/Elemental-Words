# ElementalWords

## Pre-Requisites
* .NET 8.0

## How to run
To run this program, please clone or download the repo and run inside of Visual Studo 2022

## How it works

- The program works by importing the periodic table from a JSON file and deserializing that into an 
array of the `Element` class. 
- The program will then take an input from the user comprising only of letters. 
- It will then run the `ElementalForms` method with the provided input from the user.
- It will then find every possible combination of elements from the periodic table to build that word
- Finally the response from the `ElementalForms` method will be displayed in a console output to the user

## The logic behind `FindCombinations`

A breakdown of how the `FindCombinations` method is:
1. Iterate through each of the elements in our `elements` class variable array
2. We then find each periodic table elements symbol check if the inputted word matches this symbol
3. If it does match, then we will create a new substring of the word minus the symbol we have just found, for example "Snack" would become "nack" as we found Sulfur (S) as a match
4. We then check if we're at the end of the trimmed word, if we are we can just add this element to the combinations list
5. If the word still has characters left after being trimmed, we need to run `FindCombinations` recursively with the trimmed word to find all possible combinations of elements to build the remainder of this word
6. The word is then trimmed again on each following recursion to find all possible combinations of elements
7. The whole process is then repeated again if the start of the word matches a second element in the periodic table, for example "snack" also matched "Tin" (Sn)
8. The full collection of combinations is then returned to the user and outputted in the console

## Example run
### Input
- `Word` - "Snack"

### Output
```json
[Sulfur (S)],[Nitrogen (N)],[Actinium (Ac)],[Potassium (K)]
[Sulfur (S)],[Sodium (Na)],[Carbon (C)],[Potassium (K)]
[Tin (Sn)],[Actinium (Ac)],[Potassium (K)]
```


## Notes
- `IElementService` is injected using a singleton as the object will be the same for all requests in this small program
- The list of elements are loaded once when the application starts for efficiency.
- It seems like there's multiple periodic tables based on where you are in the world, so feel free to swap out the `Elements.Json` file for your own, as long as the file you provide has "symbol" and "name" fields, regardless of other fields present

## What I would change
Below is a list of things I would have done different or changed given a larger timeframe:
- Moved the list of elements into a SQL or NO-SQL database like `MongoDB` or `Microsoft SQL Server`
- Looked at more efficient solutions to storing/caching existing results to improve efficiency