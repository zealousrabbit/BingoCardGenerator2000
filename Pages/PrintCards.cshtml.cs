using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bingo_Card_for_CodeYou.Pages
{

    //Back-end code for displaying the cards
    public class PrintCardsModel : PageModel
    {
        public string Title { get; set; }
        public int Size { get; set; }
        public bool FreeSpace { get; set; }
        public List<List<string>> Cards { get; set; }

        public void OnGet(string title, int size, int numberOfCards, string phrases, bool freeSpace)
        {
            Title = title;
            Size = size;
            FreeSpace = freeSpace;
            var phraseList = phrases.Split(',').Select(p => p.Trim()).ToList();
            int numberOfPhrases = Size * Size;

            if (FreeSpace)
            {
                numberOfPhrases -= 1; // Reduce the number of phrases for the free space
            }

            Cards = new List<List<string>>();
            var rng = new Random();

            for (int i = 0; i < numberOfCards; i++)
            {
                var cardPhrases = new List<string>(phraseList);
                Shuffle(cardPhrases, rng);
                if (FreeSpace)
                {
                    cardPhrases.Insert((Size / 2) * Size + (Size / 2), "FREE");
                }
                Cards.Add(cardPhrases.Take(numberOfPhrases).ToList());
            }
        }

        private void Shuffle<T>(List<T> list, Random rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
