using EKE.Data.DataViewModels;
using EKE.Data.Entities.Enums;
using EKE.Data.Entities.Gyopar;
using EKE.Data.Entities.Museum;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.ServiceModel;
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
        Result<List<Element>> GetSelectedRows();
        Result<List<Element>> GetAllElementsByIncluding(Expression<Func<Element, bool>> predicate, params Expression<Func<Element, object>>[] inclProp);
        Result<List<Element>> GetByPage(int page, string category, string keyword);
        Result AddElement(MuseumSM model);
        Result UpdateElement(Element model);
        Result DeleteElement(int id);

        Result<ElementCategory> GetElementCategoryById(int id);
        Result<List<ElementCategory>> GetAllElementCategories();
        Result<List<ElementCategory>> GetAllElementCategoriesByIncluding(Expression<Func<ElementCategory, bool>> predicate, params Expression<Func<ElementCategory, object>>[] inclProp);

        Result AddElementCategory(string text, string author, int parentCategoryId);
        Result UpdateElementCategory(ElementCategory model);
        Result DeleteElementCategory(int id);

        Result<List<ElementTag>> GetAllTags();
        Result AddElementTag(string text, string author);
        Result UpdateElementTag(ElementTag model);
        Result DeleteElementTag(int id);

        Result<List<Element>> Search(string keyword, int skip = 0);

        Result Update(XEditSM model);
    }

    public class MuseumService : BaseService, IMuseumService
    {
        private readonly IEntityBaseRepository<Element> _elementRepo;
        private readonly IEntityBaseRepository<ElementCategory> _elementCategoryRepo;
        private readonly IEntityBaseRepository<ElementTag> _elementTagsRepo;

        private readonly IGeneralService _generalService;
        private readonly IHostingEnvironment _environment;

        public MuseumService(
            IEntityBaseRepository<Element> elementRepository,
            IEntityBaseRepository<ElementCategory> elementCategoryRepository,
            IEntityBaseRepository<ElementTag> elementTagRepository,
            IHostingEnvironment environment,
            IGeneralService generalService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _environment = environment;
            _generalService = generalService;
            _elementCategoryRepo = elementCategoryRepository;
            _elementRepo = elementRepository;
            _elementTagsRepo = elementTagRepository;
        }

        public Result AddElement(MuseumSM model)
        {
            try
            {
                var result = _elementRepo.FindBy(x => x.Title.Trim().ToLower() == model.Element.Title.Trim().ToLower());
                if (result.Count() > 0) return new Result(ResultStatus.ALREADYEXISTS);

                var category = _elementCategoryRepo.GetById(model.SelectedCategoryId);
                if (category == null) return new Result(ResultStatus.NOT_FOUND, "Category");

                var tags = new List<ElementTag>();
                foreach (var item in model.SelectedTagId)
                {
                    var tag = _elementTagsRepo.GetById(item);
                    if (tag == null) return new Result(ResultStatus.NOT_FOUND, "Category");
                    tags.Add(tag);
                }


                var elem = model.Element;
                elem.Category = category;
                elem.MediaElement = _generalService.CreateMediaElements(model.Files, model.Element.DatePublished.Year, "1", ProjectBaseEnum.Muzeum);
                elem.Publisher = model.Publisher;
                elem.Tags = tags;

                _elementRepo.Add(elem);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result AddElementCategory(string text, string author, int parentCategoryId)
        {
            try
            {
                var result = _elementCategoryRepo.FindBy(x => x.Name.Trim().ToLower() == text.Trim().ToLower());
                if (result.Count() > 0) return new Result(ResultStatus.ALREADYEXISTS);

                var model = new ElementCategory();
                model.Author = author;
                model.Name = text;

                if (parentCategoryId != 0)
                {
                    var categoryResult = _elementCategoryRepo.GetById(parentCategoryId);
                    if (categoryResult == null) return new Result(ResultStatus.NOT_FOUND);
                    model.Parent = categoryResult;
                }

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
                var result = _elementRepo.GetByIdIncluding(id, x => x.Category, x => x.MediaElement, x => x.Tags);
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
                return new Result<List<ElementCategory>>(_elementCategoryRepo.GetAllIncluding(x => x.Parent).OrderBy(x => x.OrderNo).ToList());
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
                return new Result<List<Element>>(_elementRepo.GetAllIncluding(x => x.Category, x => x.MediaElement, x => x.Tags).ToList());
            }
            catch (Exception ex)
            {
                return new Result<List<Element>>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<List<Element>> GetSelectedRows()
        {
            try
            {
                return new Result<List<Element>>(_elementRepo.GetAllIncludingPred(x => x.Selected, x => x.Category, x => x.MediaElement, x => x.Tags).Take(12).ToList());
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
                var result = _elementRepo.GetByIdIncluding(id, x => x.Category, x => x.Tags, x => x.MediaElement);
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

        public Result<List<Element>> GetByPage(int page, string category, string keyword)
        {
            var skip = page * 12;
            try
            {
                if (String.IsNullOrEmpty(keyword))
                {
                    if (String.IsNullOrEmpty(category))
                    {
                        var resultList = _elementRepo.GetAllIncludingPred(x => !x.Selected, x => x.MediaElement, x => x.Category, x => x.Tags).Skip(skip).Take(12).ToList();
                        return new Result<List<Element>>(resultList);
                    }

                    var childCategories = _elementCategoryRepo.GetAllIncludingPred(x => x.Parent.Name.ToLower().Trim() == category.ToLower().Trim(), x => x.Parent);

                    var predicate = PredicateBuilder.New<Element>(true);
                    predicate.And(x => x.Category.Name.ToLower() == category.ToLower());
                    if (childCategories.Count() > 0)
                    {
                        foreach (var item in childCategories)
                        {
                            predicate.Or(x => x.Category.Name.ToLower() == item.Name.ToLower());
                        }
                    }

                    var result = _elementRepo.GetAllIncludingPred(predicate, x => x.MediaElement, x => x.Category, x => x.Tags).Skip(skip).Take(12).ToList();
                    return new Result<List<Element>>(result);
                }

                var searchResult = Search(keyword, skip);
                return new Result<List<Element>>(searchResult.Data);
            }
            catch (Exception ex)
            {
                return new Result<List<Element>>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result<List<ElementTag>> GetAllTags()
        {
            try
            {
                return new Result<List<ElementTag>>(_elementTagsRepo.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new Result<List<ElementTag>>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result AddElementTag(string text, string author)
        {
            try
            {
                var result = _elementTagsRepo.FindBy(x => x.Name.Trim().ToLower() == text.Trim().ToLower());
                if (result.Count() > 0) return new Result(ResultStatus.ALREADYEXISTS);

                var model = new ElementTag();
                model.Author = author;
                model.Name = text;

                _elementTagsRepo.Add(model);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result UpdateElementTag(ElementTag model)
        {
            try
            {
                var result = _elementTagsRepo.GetByIdIncluding(model.Id);
                if (result == null) return new Result(ResultStatus.NOT_FOUND);

                _elementTagsRepo.Update(model);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result DeleteElementTag(int id)
        {
            try
            {
                var result = _elementTagsRepo.GetById(id);
                if (result == null) return new Result(ResultStatus.NOT_FOUND);

                _elementTagsRepo.Delete(result);
                SaveChanges();
                return new Result(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.EXCEPTION, ex.Message);
            }
        }

        public Result<List<Element>> Search(string keyword, int skip = 0)
        {
            try
            {
                var predicate = PredicateBuilder.New<Element>(true);
                predicate.And(x => x.Title.ToLower().Contains(keyword.Trim().ToLower()));
                predicate.Or(x => x.Description.ToLower().Contains(keyword.ToLower()));
                predicate.Or(x => x.Category.Name.ToLower().Contains(keyword.ToLower()));

                var synonyms = _generalService.GetAllSynonymsByName(keyword);
                if (synonyms.Count > 0)
                {
                    foreach (var synonym in synonyms)
                    {
                        predicate.Or(x => x.Title.ToLower().Contains(keyword.Trim().ToLower()));
                        predicate.Or(x => x.Description.ToLower().Contains(synonym.Name.ToLower()));
                        predicate.Or(x => x.Category.Name.ToLower().Contains(synonym.Name.ToLower()));
                    }
                }


                var result = _elementRepo.GetAllIncludingPred(predicate, x => x.Category, x => x.MediaElement, x => x.Tags).Skip(skip).Take(12).ToList();
                return new Result<List<Element>>(result);
            }
            catch (Exception ex)
            {
                return new Result<List<Element>>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result Update(XEditSM model)
        {
            var catResult = new ElementCategory();
            var elemResult = new Element();
            switch (model.Name)
            {
                case "CategoryName":
                    catResult = _elementCategoryRepo.GetById(model.PrimaryKey);
                    if (catResult == null) return new Result(ResultStatus.NOT_FOUND);
                    catResult.Name = model.Value;
                    _elementCategoryRepo.Update(catResult);
                    SaveChanges();
                    return new Result(ResultStatus.OK);
                case "ElemTitle":
                    elemResult = _elementRepo.GetById(model.PrimaryKey);
                    if (elemResult == null) return new Result(ResultStatus.NOT_FOUND);
                    elemResult.Title = model.Value;
                    _elementRepo.Update(elemResult);
                    SaveChanges();
                    return new Result(ResultStatus.OK);
                case "ElemAuthor":
                    elemResult = _elementRepo.GetById(model.PrimaryKey);
                    if (elemResult == null) return new Result(ResultStatus.NOT_FOUND);
                    elemResult.Author = model.Value;
                    _elementRepo.Update(elemResult);
                    SaveChanges();
                    return new Result(ResultStatus.OK);
                case "ElemDescription":
                    elemResult = _elementRepo.GetById(model.PrimaryKey);
                    if (elemResult == null) return new Result(ResultStatus.NOT_FOUND);
                    elemResult.Description = model.Value;
                    _elementRepo.Update(elemResult);
                    SaveChanges();
                    return new Result(ResultStatus.OK);
                case "ElemVisible":
                    elemResult = _elementRepo.GetById(model.PrimaryKey);
                    if (elemResult == null) return new Result(ResultStatus.NOT_FOUND);
                    elemResult.Selected = Convert.ToBoolean(model.Value);
                    _elementRepo.Update(elemResult);
                    SaveChanges();
                    return new Result(ResultStatus.OK);
                default:
                    return new Result(ResultStatus.NOT_FOUND);
            }
        }
    }
}
