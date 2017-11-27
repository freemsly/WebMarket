// <copyright company="Recorded Books Inc" file="NarratorsProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NarratorsProcessor : Processor<MediaTitle>
    {
        public List<FacetMap> FacetMaps { get; set; }

        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            var contributors = item.Model.Contributors.Where(x => x.Type != null && x.Type.Equals(Constants.Facets.Narrator));
            int i = 1;
            foreach (var contributor in contributors)
            {
                string[] names = ComposeNameTokens(contributor);
                item.TokenProperties.Add(new Narrator()
                {
                    Order = i++,
                    Text = String.Intern(names[0]),
                    Facet = String.Intern(Constants.Facets.Narrator),
                    Token = String.Intern(names[1])
                });
            }
        }

        private string[] ComposeNameTokens(Contributor contributor)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbt = new StringBuilder();

            if (!String.IsNullOrWhiteSpace(contributor.LastName))
            {
                if (!String.IsNullOrWhiteSpace(contributor.FirstName))
                {
                    sb.Append(String.Format("{0} {1}", contributor.FirstName, contributor.LastName));
                    sbt.Append(String.Format("{0}-{1}", Tokenize(contributor.FirstName), Tokenize(contributor.LastName)));
                }
                else
                {
                    sb.Append(contributor.LastName);
                    sbt.Append(contributor.LastName);
                }

            }
            else if (!String.IsNullOrWhiteSpace(contributor.FirstName))
            {
                sb.Append(contributor.FirstName);
                sbt.Append(contributor.FirstName);
            }
            else
            {
                return new string[2] { "", "" };
            }
            //return new string[2] { ToTitleCase(sb.ToString()), sbt.ToString() };
            return new string[2] { sb.ToString(), sbt.ToString() };
        }
    }
}
