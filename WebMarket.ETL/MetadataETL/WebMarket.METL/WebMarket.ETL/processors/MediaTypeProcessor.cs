// <copyright company="Recorded Books Inc" file="MediaTypeProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;

    public class MediaTypeProcessor : Processor<MediaTitle>
    {
        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            string propertyValue = item.Model.Format;

            item.TokenProperties.Add(new MediaType()
            {
                Facet = String.Intern(Constants.Facets.MediaType),
                Text = String.Intern(ParseMediaType(propertyValue)),
                Token = String.Intern(ParseMediaType(propertyValue))
            });
        }

        private static string ParseMediaType(string mediatype)
        {
           // return mediatype.ToLower() == "mp3 cd" ? "MP3" : mediatype;
            return mediatype;
        }
    }
}
