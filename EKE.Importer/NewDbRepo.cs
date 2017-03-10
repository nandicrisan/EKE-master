using EKE.Importer.NewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EKE.Importer
{
    public class NewDbRepo
    {
        public int AddMagazine(string title, int categId)
        {
            try
            {
                //Split title
                var splittedTitle = title.Split('/');
                if (splittedTitle == null || splittedTitle.Count() < 2)
                {
                    Console.WriteLine(categId + ": Title splitting problem.");
                    return 0;
                }
                var tempPublisYear = 0;
                if (!int.TryParse(splittedTitle[0], out tempPublisYear))
                {
                    Console.WriteLine(categId + ": Error parsing publish year");
                    return 0;
                }
                //Check if magaine is alredy exists.
                if (GetMagazine(title) != null)
                {
                    Console.WriteLine(categId + ": Magaine alredy exist.");
                    return 0;
                }

                using (var db = new NewModels())
                {
                    //Crea new model
                    var magazine = new Magazine
                    {
                        Title = title,
                        Slug = GenerateSlug(title),
                        PublishYear = tempPublisYear,
                        PublishSection = splittedTitle[1],
                        DateCreated = DateTime.Now,
                        CategoryId = 7
                    };
                    //WriteMagazine(magazine);
                    db.Magazines.Add(magazine);
                    db.SaveChanges();
                    return magazine.Id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(categId + ": " + ex.Message);
                return 0;
            }
        }

        public Article AddArticle(int magaId, string title, string authorName, string content, string slug, List<KeyValuePair<string, string>> mediaElements)
        {
            try
            {
                //Create model
                using (var db = new NewModels())
                {
                    //Check author
                    var author = GetAddAuthor(authorName);
                    var article = new Article()
                    {
                        Title = title,
                        Content = content,
                        Slug = slug,
                        MagazineId = magaId,
                        DateCreated = DateTime.Now
                    };
                    if (author != null)
                        article.AuthorId = author.Id;
                    db.Articles.Add(article);
                    db.SaveChanges();
                    if (mediaElements != null && mediaElements.Count() > 0)
                    {
                        foreach (var media in mediaElements)
                        {
                            db.MediaElements.Add(new MediaElement
                            {
                                ArticleId = article.Id,
                                MagazineId = magaId,
                                Description = media.Value,
                                OriginalName = media.Key,
                                Type = 1,
                            });
                        }
                        db.SaveChanges();
                    }
                    return article;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(magaId + " - Error on article add: " + ex.Message);
                return null;
            }
        }

        public Author GetAddAuthor(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return null;
                using (var db = new NewModels())
                {
                    var author = db.Authors.FirstOrDefault(p => p.Name.Trim().ToLower() == name.Trim().ToLower());
                    if (author != null)
                        return author;
                    //If outhor not found in db then create
                    var newAuthor = new Author
                    {
                        Name = name
                    };
                    db.Authors.Add(newAuthor);
                    db.SaveChanges();
                    return newAuthor;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on author check-" + ex.Message);
                return null;
            }
        }

        public Magazine GetMagazine(string title)
        {
            try
            {
                using (var db = new NewModels())
                {
                    return db.Magazines.FirstOrDefault(p => p.Title == title);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void WriteMagazine(Magazine model)
        {
            Console.WriteLine(string.Format("[Id:{0}, Title{1}, PublisYear:{2}, PublishSection:{3}, Slug:{4} ]", model.Id, model.Title, model.PublishYear, model.PublishSection, model.Slug));
        }

        #region General
        public string GenerateSlug(string title)
        {
            string str = RemoveAccent(title).Replace(" / ", "_").ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "_");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
        #endregion
    }
}
