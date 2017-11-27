// <copyright company="Recorded Books Inc" file="TitleProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;

    public sealed class TitleProcessor : Processor<MediaTitle>
    {
        private const char c = ':';

        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            string title = String.Empty;
            string subtitle = String.Empty;

            if (String.IsNullOrWhiteSpace(item.Model.Subtitle) && !String.IsNullOrWhiteSpace(item.Model.Title))
            {
                    title = item.Model.Title;
            }
            else if (String.IsNullOrWhiteSpace(item.Model.Title) && !String.IsNullOrWhiteSpace(item.Model.Subtitle))
            {
                subtitle = item.Model.Subtitle;
                item.AddError("title", "Title is missing, has only subtitle");
            }
            else 
            {
                title = item.Model.Title.Trim();
                subtitle = item.Model.Subtitle.Trim();

                item.SimpleProperties.Add((new TypedItem(String.Intern(Constants.Facets.Fulltitle), item.Model.Title + " " +  item.Model.Subtitle)));
            }

            if (!String.IsNullOrWhiteSpace(title))
            {
                string token = Tokenize(title);
                item.TokenProperties.Add(new Title()
                {
                    Text = title,
                    Token = token
                });
            }
            if (!String.IsNullOrWhiteSpace(subtitle))
            {
                item.SimpleProperties.Add((new TypedItem(String.Intern(Constants.Facets.Subtitle), subtitle)));
            }
        }
    }
    
}
