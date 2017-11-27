// <copyright company="Recorded Books Inc" file="Common.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Common
{
    using System;

    public static class Util
    {
        private static readonly Random Random = new Random();
        public static int GeneratorRandomInteger(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }
    }
}
