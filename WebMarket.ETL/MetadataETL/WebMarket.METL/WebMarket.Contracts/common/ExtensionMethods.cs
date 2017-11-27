// <copyright company="Recorded Books Inc" file="ExtensionMethods.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WebMarket.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ExtensionMethods
    {
        public static string ToLetters(this string input)
        {
            StringBuilder sb = new StringBuilder(input.Length);
            if (!String.IsNullOrWhiteSpace(input))
            {
                char[] arr = input.Trim().ToCharArray();
                int max = arr.Length;
                for (int i = 0; i < max; i++)
                {
                    char c = arr[i];
                    if (i > 0 && Char.IsWhiteSpace(c))
                    {
                        if (!Char.IsWhiteSpace(arr[i - 1]))
                        {
                            sb.Append(c);
                        }
                    }
                    else if (Char.IsLetter(c))
                    {
                        sb.Append(c);
                    }
                }
            }
            return sb.ToString();
        }

        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(this IEnumerable<TSource> source,int batchSize)
        {
            var batch = new List<TSource>();
            foreach (var item in source)
            {
                batch.Add(item);
                if (batch.Count == batchSize)
                {
                    yield return batch;
                    batch = new List<TSource>();
                }
            }

            if (batch.Any()) yield return batch;
        }

        public class GenericKeyedCollection<TKey, TItem> : KeyedCollection<TKey, TItem>
        {
            private readonly Func<TItem, TKey> _getKeyFunc;
            protected override TKey GetKeyForItem(TItem item)
            {
                return _getKeyFunc(item);
            }

            public GenericKeyedCollection(Func<TItem, TKey> getKeyFunc)
            {
                _getKeyFunc = getKeyFunc;
            }
        }

        public static Stream ToStream(this string @this)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@this);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        public static T ParseXML<T>(this string @this) where T : class
        {
            var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }

    }
}
