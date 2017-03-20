using EKE.Importer.NewModel;
using HtmlAgilityPack;
using System;
using System.Linq;

namespace EKE.Importer
{
    public class HtmlFormatter
    {
        public void ReplaceBr()
        {
            try
            {
                using (var db = new NewModels())
                {
                    var pages = 555;
                    var items = 0;
                    HtmlDocument doc = new HtmlDocument();
                    for (int i = 0; i < pages; i++)
                    {
                        var articles = db.Articles.Where(p=>string.IsNullOrEmpty(p.OldContent)).OrderBy(p=>p.Id).Skip(i * 10).Take(10).ToList();
                        foreach (var article in articles)
                        {
                            article.OldContent = article.Content;
                            article.Content = article.Content.Replace("<br />", Environment.NewLine);
                            doc.LoadHtml(article.Content);
                            var found1 = doc.DocumentNode.Descendants("span").Where(d => d.Attributes.Contains("class") && Constants.AuthorSearchIn.Contains(d.Attributes["class"].Value));
                            //Check in span
                            if (found1.Count() != 0)
                            {
                                found1.FirstOrDefault().Remove();
                            }
                            found1 = doc.DocumentNode.Descendants("p").Where(d => d.Attributes.Contains("class") && Constants.AuthorSearchIn.Contains(d.Attributes["class"].Value));
                            //Check in paragraph too
                            if (found1.Count() != 0)
                            {
                                found1.FirstOrDefault().Remove();
                            }
                            article.Content = doc.DocumentNode.InnerText;
                            items++;
                        }
                        db.SaveChanges();
                        Console.Clear();
                        Console.WriteLine((items * 100) / 5544);
                    }
                    Console.WriteLine("end");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
