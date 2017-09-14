using EKE.Data.Entities;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.Utils;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;
using EKE.Data.Entities.Gyopar;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using EKE.Data.Entities.Enums;
using ImageMagick;

namespace EKE.Service.Services
{
    public interface IGeneralService : IBaseService
    {
        string GenerateSlug(string phrase, int year, string section);
        string RemoveAccent(string txt);
        string RandomString(int length);

        List<MediaElement> CreateMediaElements(ICollection<IFormFile> files, int year, string section, ProjectBaseEnum project);
    }

    public class GeneralService : BaseService, IGeneralService
    {
        private readonly IHostingEnvironment _environment;

        public GeneralService(
            IHostingEnvironment environment,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _environment = environment;
        }

        #region General
        public string GenerateSlug(string phrase, int year, string section)
        {
            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            str = string.Format("{0}_{1}_{2}", str, year, section);
            return str;
        }
        public string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        #region MediaElements
        public List<MediaElement> CreateMediaElements(ICollection<IFormFile> files, int year, string section, ProjectBaseEnum project)
        {
            var gyoparPath = "";
            var scope = MediaTypesScope.Cover;
            switch (project)
            {
                case ProjectBaseEnum.Gyopar:
                    gyoparPath = _environment.WebRootPath.Replace("EKE-Admin.Web", "EKE-Gyopar.Web");
                    break;
                case ProjectBaseEnum.Muzeum:
                    gyoparPath = _environment.WebRootPath.Replace("EKE-Admin.Web", "EKE-Muzeum.Web");
                    scope = MediaTypesScope.Museum;
                    break;
                default:
                    break;
            }

            var relativePath = String.Format("Uploads/{0}/{1}", year.ToString().Trim(), section.Trim());
            var uploads = Path.Combine(gyoparPath, relativePath);
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var mediaElements = new List<MediaElement>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var uploadPath = Path.Combine(uploads, file.FileName);
                    using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    var fileName = String.Format("r1_{0}", file.FileName);
                    var outputPath = Path.Combine(uploads, fileName);
                    var resize = ResizeImage(uploadPath, outputPath, scope);

                    var mediaElem = new MediaElement();
                    mediaElem.OriginalName = String.Format("{0}/{1}", relativePath, resize.IsOk() ? fileName : file.FileName);
                    mediaElem.Description = string.Format("{0}_{1}", year.ToString().Trim(), section.Trim());
                    mediaElem.Name = RandomString(10);
                    mediaElem.Type = MediaTypesEnum.Image;
                    mediaElem.Scope = scope;
                    mediaElements.Add(mediaElem);
                }
            }

            return mediaElements;
        }

        public Result ResizeImage(string inputPath, string outputPath, MediaTypesScope type)
        {
            var size = 0;
            var quality = 0;

            switch (type)
            {
                case MediaTypesScope.Background:
                    break;
                case MediaTypesScope.Cover:
                    size = 400;
                    quality = 75;
                    break;
                case MediaTypesScope.Article:
                    break;
                case MediaTypesScope.Museum:
                    size = 800;
                    quality = 75;
                    break;
                default:
                    break;
            }

            try
            {
                using (var image = new MagickImage(inputPath))
                {
                    image.Resize(size, size);
                    image.Strip();
                    image.Quality = quality;

                    if (type == MediaTypesScope.Museum)
                    {
                        using (MagickImage watermark = new MagickImage(@"C:\Users\ldeak\Source\Repos\EKE-master\src\EKE-Gyopar.Web\wwwroot\images\logo\eke.gif"))
                        {
                            // Optionally make the watermark more transparent
                            watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 1.5);

                            // Or draw the watermark at a specific location
                            image.Composite(watermark, Gravity.Southeast, CompositeOperator.Over);
                        }
                    }
                    image.Write(outputPath);
                    return new Result(ResultStatus.OK);
                }
            }
            catch (Exception)
            {
                return new Result(ResultStatus.EXCEPTION);
            }
        }
        #endregion
    }
}
