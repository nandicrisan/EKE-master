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
using EKE.Service.ServiceModel;

namespace EKE.Service.Services
{
    public interface IGeneralService
    {
        string GenerateSlug(string phrase, int year, string section);
        string RemoveAccent(string txt);
        string RandomString(int length);

        List<MediaElement> CreateMediaElements(ICollection<IFormFile> files, int year, string section, ProjectBaseEnum project);

        Result<List<Synonym>> GetAllSynonyms();
        List<Synonym> GetAllSynonymsByName(string name);
        Result<Synonym> AddSynonym(string text);
        Result DeleteSynonym(int id);
        Result ConnectSynonym(int id, string text);
        Result UpdateSynonym(XEditSM model);
    }

    public class GeneralService : IGeneralService
    {
        private readonly IHostingEnvironment _environment;
        private readonly IUnitOfWork _unitOfWork;

        public GeneralService(
            IHostingEnvironment environment,
            IUnitOfWork unitOfWork)
        {
            _environment = environment;
            _unitOfWork = unitOfWork;
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

            var relativePath = $"Uploads/{year.ToString().Trim()}/{section.Trim()}";
            var uploads = Path.Combine(gyoparPath, relativePath);
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var mediaElements = new List<MediaElement>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var mediaElem = new MediaElement();
                    mediaElem.Name = String.Format("{0}_{1}", RandomString(10), file.FileName);

                    var uploadPath = Path.Combine(uploads, mediaElem.Name);
                    using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    var fileName = String.Format("r1_{0}", mediaElem.Name);
                    var outputPath = Path.Combine(uploads, fileName);
                    var resize = ResizeImage(uploadPath, outputPath, scope);

                    mediaElem.OriginalName = String.Format("{0}/{1}", relativePath, resize.IsOk() ? fileName : mediaElem.Name);
                    mediaElem.Description = string.Format("{0}_{1}", year.ToString().Trim(), section.Trim());
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
                        using (MagickImage watermark = new MagickImage(@"C:\EKE-Backup\Images\eke.gif"))
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

        #region Synonyms
        public Result<List<Synonym>> GetAllSynonyms()
        {
            var result = _unitOfWork.SynonymRepository.GetAllIncludingPred(x => x.Main, x => x.Synonyms).ToList();
            return new Result<List<Synonym>>(result);
        }

        public Result<Synonym> AddSynonym(string text)
        {
            var exists = _unitOfWork.SynonymRepository.FindBy(x => x.Name == text);
            if (exists.Any()) return new Result<Synonym>(ResultStatus.ALREADYEXISTS);

            var model = new Synonym
            {
                Name = text,
                Main = true
            };

            _unitOfWork.SynonymRepository.Add(model);
            _unitOfWork.SaveChanges();
            return new Result<Synonym>(model);
        }

        public Result DeleteSynonym(int id)
        {
            var result = _unitOfWork.SynonymRepository.GetByIdIncluding(id, x => x.Synonyms);
            if (result == null) return new Result(ResultStatus.NOT_FOUND);

            foreach (var item in result.Synonyms)
            {
                _unitOfWork.SynonymRepository.Delete(item);
                _unitOfWork.SaveChanges();
            }

            _unitOfWork.SynonymRepository.Delete(result);
            _unitOfWork.SaveChanges();
            return new Result(ResultStatus.OK);
        }

        public Result ConnectSynonym(int id, string text)
        {
            var result = _unitOfWork.SynonymRepository.GetByIdIncluding(id, x => x.Synonyms);
            if (result == null) return new Result(ResultStatus.NOT_FOUND);

            var newSynonym = new Synonym
            {
                Name = text,
                Main = false
            };

            result.Synonyms.Add(newSynonym);
            _unitOfWork.SynonymRepository.Update(result);
            _unitOfWork.SaveChanges();
            return new Result(ResultStatus.OK);
        }

        public Result UpdateSynonym(XEditSM model)
        {
            var result = _unitOfWork.SynonymRepository.GetByIdIncluding(model.PrimaryKey, x => x.Synonyms);
            if (result == null) return new Result(ResultStatus.NOT_FOUND);

            if (String.IsNullOrEmpty(model.Value))
            {
                _unitOfWork.SynonymRepository.Delete(result);
                _unitOfWork.SaveChanges();
                return new Result(ResultStatus.OK);
            }

            result.Name = model.Value;
            _unitOfWork.SynonymRepository.Update(result);
            _unitOfWork.SaveChanges();
            return new Result(ResultStatus.OK);
        }

        public List<Synonym> GetAllSynonymsByName(string name)
        {
            var result = _unitOfWork.SynonymRepository.GetAllIncludingPred(x => x.Name == name.Trim(), x => x.Synonyms).FirstOrDefault();
            if (result == null) return new List<Synonym>();
            if (result.Synonyms.Count == 0)
            {
                result = _unitOfWork.SynonymRepository.GetAllIncluding(x => x.Synonyms).FirstOrDefault(e => e.Synonyms
                                 .Any(a => a.Name == name));
                if (result == null) return new List<Synonym>();
                result.Synonyms = result.Synonyms.Where(x => x.Name != name).ToList();
                var list = result.Synonyms;
                result.Synonyms = null;
                list.Add(result);
                return list.ToList();
            }

            return result.Synonyms.ToList();
        }
        #endregion
    }
}
