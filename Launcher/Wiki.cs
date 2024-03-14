using Genbox.Wikipedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Genbox.Wikipedia;
using Genbox.Wikipedia.Enums;
using Genbox.Wikipedia.Objects;

namespace Launcher
{
    public static class Wiki
    {
        public static async Task SearchAsync(string searchTerm)
        {
            //Default language is English
            WikipediaClient client = new WikipediaClient();

            using (client)
            {
                WikiSearchRequest req = new WikiSearchRequest(searchTerm);
                req.Limit = 5; //We would like 5 results
                req.WhatToSearch = WikiWhat.Text; //We would like to search inside the articles

                WikiSearchResponse resp = await client.SearchAsync(req).ConfigureAwait(false);

                Console.WriteLine($"Searching for {req.Query}");
                Console.WriteLine();
                Console.WriteLine($"Found {resp.QueryResult.SearchResults.Count} English results:");


                foreach (SearchResult s in resp.QueryResult.SearchResults)
                {
                    Console.WriteLine($" - {s.Title}");
                }

                Console.WriteLine();
                Console.WriteLine();

                //We change the language to Spanish
                req.WikiLanguage = WikiLanguage.Spanish;

                resp = await client.SearchAsync(req).ConfigureAwait(false);

                Console.WriteLine($"Found {resp.QueryResult.SearchResults.Count} Spanish results:");

                foreach (SearchResult s in resp.QueryResult.SearchResults)
                {
                    Console.WriteLine($" - {s.Title}");
                }
            }
        }
    }
}
