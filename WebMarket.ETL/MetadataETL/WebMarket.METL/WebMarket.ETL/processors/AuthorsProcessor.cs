// <copyright company="Recorded Books Inc" file="AuthorsProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;
    using System.Linq;
    using System.Text;

    public class AuthorsProcessor : Processor<MediaTitle>
    {
        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            var contributors = item.Model.Contributors.Where(x => x.Type != null && x.Type.Equals(Constants.Facets.Author));
            
            int i = 1;
            foreach (var contributor in contributors)
            {
                string[] names = ComposeNameTokens(contributor);
                if (i == 1)
                {
                    item.SimpleProperties.Add(new TypedItem(Constants.Facets.SortAuthor, names[2]));
                }
                item.TokenProperties.Add(new Author()
                {
                    Order = i++,
                    Text = String.Intern(names[0]),
                    Facet = String.Intern(Constants.Facets.Author),
                    Token = String.Intern(names[1])
                });
            }


        }

        private string[] ComposeNameTokens(Contributor contributor)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbt = new StringBuilder();
            StringBuilder sbLastName = new StringBuilder();

            if (!String.IsNullOrWhiteSpace(contributor.LastName))
            {
                if (!String.IsNullOrWhiteSpace(contributor.FirstName) && !String.IsNullOrWhiteSpace(contributor.FirstName.Trim(new char[] { '-' })))
                {
                    sb.Append(String.Format("{0} {1}", contributor.FirstName, contributor.LastName));
                    sbt.Append(String.Format("{0}-{1}", Tokenize(contributor.FirstName), Tokenize(contributor.LastName)));                    
                }
                else
                {
                    sb.Append(contributor.LastName);
                    sbt.Append(contributor.LastName);
                }
                sbLastName.Append(contributor.LastName);
            }
            else if (!String.IsNullOrWhiteSpace(contributor.FirstName))
            {
                sb.Append(contributor.FirstName);
                sbt.Append(contributor.FirstName);
                sbLastName.Append(contributor.FirstName);
            }
            else if  (!String.IsNullOrWhiteSpace(contributor.Organisation))
            {
                sb.Append(contributor.Organisation);
                sbt.Append(contributor.Organisation);
                sbLastName.Append(contributor.Organisation);
            }
            else
            {
                return new string[3] { "", "" ,""};
            }
            //return new string[2] { ToTitleCase(sb.ToString()), sbt.ToString() };
            return new string[3] { sb.ToString(), sbt.ToString(), sbLastName.ToString()};
        }
    }
}
