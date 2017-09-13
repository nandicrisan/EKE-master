using EKE.Data.DataViewModels;
using EKE.Data.Entities.Gyopar;
using EKE.Data.Entities.Museum;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.Utils;
using LinqKit;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EKE.Service.Services.Admin.Muzeum
{
    public interface IMuseumService : IBaseService
    {
        Result<Element> GetElementById(int id);
        Result<List<Element>> GetAllElements();
        Result<List<Element>> GetAllElementsByIncluding(Expression<Func<Element, bool>> predicate, params Expression<Func<Element, object>>[] inclProp);

        Result AddElement(Element model);
        Result UpdateElement(Element model);
        Result DeleteElement(int id);

        Result<ElementCategory> GetElementCategoryById(int id);
        Result<List<ElementCategory>> GetAllElementCategories();
        Result<List<ElementCategory>> GetAllElementCategoriesByIncluding(Expression<Func<ElementCategory, bool>> predicate, params Expression<Func<ElementCategory, object>>[] inclProp);

        Result AddElementCategory(string text, string author);
        Result UpdateElementCategory(ElementCategory model);
        Result DeleteElementCategory(int id);
    }

    public class MuseumService : BaseService, IMuseumService
    {
        private readonly IEntityBaseRepository<Element> _elementRepo;
        private readonly IEntityBaseRepository<ElementCategory> _elementCategoryRepo;

        private readonly IGeneralService _generalService;
        private readonly IHostingEnvironment _environment;

        public MuseumService(
            IEntityBaseRepository<Element> elementRepository,
            IEntityBaseRepository<ElementCategory> elementCategoryRepository,
            IHostingEnvironment environment,
            IGeneralService generalService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _environment = environment;
            _generalService = generalService;
            _elementCategoryRepo = elementCategoryRepository;
            _elementRepo = elementRepository;
        }

        public Result AddElement(Element model)
        {
            try
            {
                var result = _elementRepo.FindBy(x => x.Title.Trim().ToLower() == model.Title.Trim().ToLower());
                if (result.Count() > 0) return new Result(ResultStatus.ALREADYEXISTS);

                _elementRepo.Add(model);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result AddElementCategory(string text, string author)
        {
            try
            {
                var result = _elementCategoryRepo.FindBy(x => x.Name.Trim().ToLower() == text.Trim().ToLower());
                if (result.Count() > 0) return new Result(ResultStatus.ALREADYEXISTS);

                var model = new ElementCategory();
                model.Author = author;
                model.Name = text;

                _elementCategoryRepo.Add(model);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result DeleteElement(int id)
        {
            try
            {
                var result = _elementRepo.GetById(id);
                if (result == null) return new Result(ResultStatus.NOT_FOUND);

                _elementRepo.Delete(result);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result DeleteElementCategory(int id)
        {
            try
            {
                var result = _elementCategoryRepo.GetById(id);
                if (result == null) return new Result(ResultStatus.NOT_FOUND);

                _elementCategoryRepo.Delete(result);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result<List<ElementCategory>> GetAllElementCategories()
        {
            try
            {
                return new Result<List<ElementCategory>>(_elementCategoryRepo.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new Result<List<ElementCategory>>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<List<ElementCategory>> GetAllElementCategoriesByIncluding(Expression<Func<ElementCategory, bool>> predicate, params Expression<Func<ElementCategory, object>>[] inclProp)
        {
            try
            {
                var result = _elementCategoryRepo.GetAllIncludingPred(predicate, inclProp).ToList();
                return new Result<List<ElementCategory>>(result);
            }
            catch (Exception ex)
            {
                return new Result<List<ElementCategory>>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<List<Element>> GetAllElements()
        {
            try
            {
                return new Result<List<Element>>(_elementRepo.GetAllIncluding(x => x.Category).ToList());
            }
            catch (Exception ex)
            {
                return new Result<List<Element>>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<List<Element>> GetAllElementsByIncluding(Expression<Func<Element, bool>> predicate, params Expression<Func<Element, object>>[] inclProp)
        {
            try
            {
                var result = _elementRepo.GetAllIncludingPred(predicate, inclProp).ToList();
                return new Result<List<Element>>(result);
            }
            catch (Exception ex)
            {
                return new Result<List<Element>>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<Element> GetElementById(int id)
        {
            try
            {
                var result = _elementRepo.GetByIdIncluding(id, x => x.Category);
                if (result == null) return new Result<Element>(ResultStatus.NOT_FOUND);
                return new Result<Element>(result);
            }
            catch (Exception ex)
            {
                return new Result<Element>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result<ElementCategory> GetElementCategoryById(int id)
        {
            try
            {
                var result = _elementCategoryRepo.GetByIdIncluding(id);
                if (result == null) return new Result<ElementCategory>(ResultStatus.NOT_FOUND);
                return new Result<ElementCategory>(result);
            }
            catch (Exception ex)
            {
                return new Result<ElementCategory>(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result UpdateElement(Element model)
        {
            try
            {
                var result = _elementRepo.GetByIdIncluding(model.Id, x => x.MediaElement, x => x.Category);
                if (result == null) return new Result(ResultStatus.NOT_FOUND);

                _elementRepo.Update(model);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result UpdateElementCategory(ElementCategory model)
        {
            try
            {
                var result = _elementCategoryRepo.GetByIdIncluding(model.Id);
                if (result == null) return new Result(ResultStatus.NOT_FOUND);

                _elementCategoryRepo.Update(model);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }
    }
}
