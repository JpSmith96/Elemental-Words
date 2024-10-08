﻿using ElementalWords.Classes.Element;
using System.Text.Json;

namespace ElementalWords.Services
{
    public class ElementService : IElementService
    {
        private Element[] elements = [];
        private readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// Gets the elements from the json file and deserializes them into the elements array.
        /// </summary>
        /// <returns></returns>
        public bool InitializeElements()
        {
            try
            {
                var filePath = Path.Combine(Environment.CurrentDirectory, @"Data\", "elements.json");

                var jsonString = File.ReadAllText(filePath);

                var elements = JsonSerializer.Deserialize<Element[]>(jsonString, jsonOptions);

                if (elements == null) return false;

                this.elements = elements;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Public exposed method for finding combinations of words
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public ICollection<ElementResponse> ElementalForms(string word)
        {
            try
            {
                return FindCombinations(word.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing combinations of elements for word {word}", ex);
            }
        }


        /// <summary>
        /// Recursive function designed to find all possible combinations of elements to build the argument word
        /// </summary>
        /// <param name="remainingWord"></param>
        /// <returns></returns>
        private List<ElementResponse> FindCombinations(string remainingWord)
        {
            var combinations = new List<ElementResponse>();

            foreach (var element in elements)
            {
                //iterate through element and check if the remainder of the word starts with the element symbol
                if (remainingWord.StartsWith(element.Symbol, StringComparison.CurrentCultureIgnoreCase))
                {
                    //create a new substring of the remaining word that excludes the element symbol we just found
                    var newRemainingWord = remainingWord[element.Symbol.Length..];

                    //if we've reached the end of the word, add the element to the combinations list
                    if (string.IsNullOrWhiteSpace(newRemainingWord))
                    {
                        combinations.Add(new ElementResponse
                        {
                            Elements = new List<Element> { element }
                        });
                    }
                    //otherwise, recursively call FindCombinations with the new remaining word to find the other elements that make up the word
                    else
                    {
                        var subCombinations = FindCombinations(newRemainingWord);

                        foreach (var subCombination in subCombinations)
                        {
                            var newResponse = new ElementResponse
                            {
                                Elements = new List<Element> { element }
                            };

                            foreach (var subElement in subCombination.Elements)
                            {
                                newResponse.Elements.Add(subElement);
                            }
                            combinations.Add(newResponse);
                        }
                    }
                }
            }

            return combinations;
        }
    }
}
