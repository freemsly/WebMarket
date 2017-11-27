// <copyright company="Recorded Books Inc" file="Tokenizer.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    using System;
    using System.Collections.Generic;

    public static class Tokenizer
    {
        public static string Tokenize(string rawInput)
        {
            if (rawInput != null)
            {
                List<string> list = new List<string>();
                list.Add(rawInput.Trim());
                list.Add(list[list.Count - 1].ToLetters());
                list.Add(list[list.Count - 1].Replace("  ", " "));
                list.Add(list[list.Count - 1].Replace(' ', '-'));
                string outputToken = list[list.Count - 1].ToLower();
                return outputToken;
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
