using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EKE.Importer
{
    public class Importer
    {
        public bool FoundInThis { get; set; }
        public List<int> AuthorNotFoundInCategory { get; set; }
        public static List<int> OkMagazine { get; set; }

        public Importer()
        {
            FoundInThis = false;
            AuthorNotFoundInCategory = new List<int>();
            OkMagazine = new List<int>();
        }

        public void Start()
        {
            var repo = new NewDbRepo();
            using (var db = new OldModels.Models())
            {
                HtmlDocument doc = new HtmlDocument();
                var categories = db.jos_categories.Take(2).ToList();
                foreach (var categ in categories)
                {
                    if (Constants.ToSkip.Contains(categ.id))
                        continue;
                    //Add magaine to database
                    var magId = repo.AddMagazine(categ.title, categ.id);
                    if (magId == 0)
                        continue;
                    var contents = db.jos_content.Where(p => p.catid == categ.id);
                    foreach (var item in contents)
                    {
                        ImportStatistic.Examined++;
                        doc.LoadHtml(item.introtext);
                        var authorName = string.Empty;
                        var found = doc.DocumentNode.Descendants("span").Where(d => d.Attributes.Contains("class") && Constants.AuthorSearchIn.Contains(d.Attributes["class"].Value));
                        //Check in paragraph too
                        if (found.Count() == 0)
                            found = doc.DocumentNode.Descendants("p").Where(d => d.Attributes.Contains("class") && Constants.AuthorSearchIn.Contains(d.Attributes["class"].Value));
                        else
                        {
                            authorName = found.FirstOrDefault().InnerText;
                        }

                        if (found.Count() != 0)
                        {
                            ImportStatistic.AuthorFound++;
                            FoundInThis = true;
                            authorName = found.FirstOrDefault().InnerText;
                        }
                        //Check if we have images
                        var images = doc.DocumentNode.Descendants("img");
                        var pairs = new List<KeyValuePair<string, string>>();
                        if (images.Count() > 0)
                        {
                            foreach (var img in images)
                            {
                                pairs.Add(new KeyValuePair<string, string>
                               (
                                    img.Attributes["src"].Value,
                                    img.Attributes["alt"].Value

                                ));
                            }
                        }
                        //Add article
                        repo.AddArticle(magId, item.title, authorName, item.introtext, item.alias, pairs);
                        //WriteStatistics();
                    }
                    //if (!FoundInThis)
                    //    AuthorNotFoundInCategory.Add(categ.id);
                    //else
                    //    OkMagazine.Add(categ.id);
                    //FoundInThis = false;
                }
                Console.WriteLine("Done!");
                //WriteNotFound();
                Console.ReadKey();
            }
        }
        public static void WriteStatistics()
        {
            Console.Clear();
            Console.WriteLine("Examined: " + ImportStatistic.Examined);
            Console.WriteLine("Author found: " + ImportStatistic.AuthorFound);
            Console.WriteLine("Author percentage: " + ((ImportStatistic.AuthorFound * 100) / 5543));
            Console.WriteLine("Magazine percentage: " + ((OkMagazine.Count() * 100) / (467 - Constants.Empty.Count() - Constants.Problematic.Count)));
        }

        public void WriteNotFound()
        {
            Console.Write("No author categories: ");
            foreach (var catId in AuthorNotFoundInCategory)
            {
                Console.Write(catId + ", ");
            }
        }
    }
}
