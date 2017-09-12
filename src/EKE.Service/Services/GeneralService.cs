using EKE.Data.Entities;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.Utils;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace EKE.Service.Services
{
    public interface IGeneralService : IBaseService
    {
        string GenerateSlug(string phrase, int year, string section);
        string RemoveAccent(string txt);
        string RandomString(int length);
    }

    public class GeneralService : BaseService, IGeneralService
    {
        public GeneralService(
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
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
    }
}
