using EKE.Data.Entities.Enums;
using EKE.Data.Entities.Museum;
using EKE.Data.Infrastructure;
using EKE.Data.Repository;
using EKE.Service.ServiceModel;
using EKE.Service.Utils;
using LinqKit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EKE.Service.Services.Admin.Muzeum
{
    public interface IMuseumService
    {
        Result<Element> GetElementById(int id);
        Result<Element> GetNeighbourElementById(int id, bool nextTo);
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

        Result<Element> UpdateCover(ICollection<IFormFile> files, int id);
    }

    public class MuseumService : IMuseumService
    {
        private readonly IGeneralService _generalService;
        private readonly IUnitOfWork _unitOfWork;

        public MuseumService(
            IGeneralService generalService,
            IUnitOfWork unitOfWork)
        {
            _generalService = generalService;
            _unitOfWork = unitOfWork;
        }

        public Result AddElement(MuseumSM model)
        {
            try
            {
                var result = _unitOfWork.ElementRepository.FindBy(x => x.Title.Trim().ToLower() == model.Element.Title.Trim().ToLower());
                if (result.Any()) return new Result(ResultStatus.ALREADYEXISTS);

                var category = _unitOfWork.ElementCategoryRepository.GetById(model.SelectedCategoryId);
                if (category == null) return new Result(ResultStatus.NOT_FOUND, "Category");

                var tags = new List<ElementTag>();
                foreach (var item in model.SelectedTagId)
                {
                    var tag = _unitOfWork.ElementTagRepository.GetById(item);
                    if (tag == null) return new Result(ResultStatus.NOT_FOUND, "Category");
                    tags.Add(tag);
                }

                var elem = model.Element;
                elem.Category = category;
                elem.MediaElement = _generalService.CreateMediaElements(model.Files, model.Element.DatePublished.Year, "1", ProjectBaseEnum.Muzeum);
                elem.Publisher = model.Publisher;
                elem.Tags = tags;

                _unitOfWork.ElementRepository.Add(elem);
                _unitOfWork.SaveChanges();
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
                var result = _unitOfWork.ElementCategoryRepository.FindBy(x => x.Name.Trim().ToLower() == text.Trim().ToLower());
                if (result.Any()) return new Result(ResultStatus.ALREADYEXISTS);

                var model = new ElementCategory
                {
                    Author = author,
                    Name = text
                };

                if (parentCategoryId != 0)
                {
                    var categoryResult = _unitOfWork.ElementCategoryRepository.GetById(parentCategoryId);
                    if (categoryResult == null) return new Result(ResultStatus.NOT_FOUND);
                    model.Parent = categoryResult;
                }

                _unitOfWork.ElementCategoryRepository.Add(model);
                _unitOfWork.SaveChanges();
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
                var result = _unitOfWork.ElementRepository.GetByIdIncluding(id, x => x.Category, x => x.MediaElement, x => x.Tags);
                if (result == null) return new Result(ResultStatus.NOT_FOUND);

                _unitOfWork.ElementRepository.Delete(result);
                _unitOfWork.SaveChanges();
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
                var result = _unitOfWork.ElementCategoryRepository.GetById(id);
                if (result == null) return new Result(ResultStatus.NOT_FOUND);

                _unitOfWork.ElementCategoryRepository.Delete(result);
                _unitOfWork.SaveChanges();
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
                return new Result<List<ElementCategory>>(_unitOfWork.ElementCategoryRepository.GetAllIncluding(x => x.Parent).OrderBy(x => x.OrderNo).ToList());
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
                var result = _unitOfWork.ElementCategoryRepository.GetAllIncludingPred(predicate, inclProp).ToList();
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
                return new Result<List<Element>>(_unitOfWork.ElementRepository.GetAllIncluding(x => x.Category, x => x.MediaElement, x => x.Tags).ToList());
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
                return new Result<List<Element>>(_unitOfWork.ElementRepository.GetAllIncludingPred(x => x.Selected, x => x.Category, x => x.MediaElement, x => x.Tags).Take(12).ToList());
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
                var result = _unitOfWork.ElementRepository.GetAllIncludingPred(predicate, inclProp).ToList();
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
                var result = _unitOfWork.ElementRepository.GetByIdIncluding(id, x => x.Category, x => x.Tags, x => x.MediaElement);
                return result == null ? new Result<Element>(ResultStatus.NOT_FOUND) : new Result<Element>(result);
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
                var result = _unitOfWork.ElementCategoryRepository.GetByIdIncluding(id);
                return result == null ? new Result<ElementCategory>(ResultStatus.NOT_FOUND) : new Result<ElementCategory>(result);
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
                var result = _unitOfWork.ElementRepository.GetByIdIncluding(model.Id, x => x.MediaElement, x => x.Category);
                if (result == null) return new Result(ResultStatus.NOT_FOUND);

                _unitOfWork.ElementRepository.Update(model);
                _unitOfWork.SaveChanges();
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
                var result = _unitOfWork.ElementCategoryRepository.GetByIdIncluding(model.Id);
                if (result == null) return new Result(ResultStatus.NOT_FOUND);

                _unitOfWork.ElementCategoryRepository.Update(model);
                _unitOfWork.SaveChanges();
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
                        var resultList = _unitOfWork.ElementRepository.GetAllIncludingPred(x => !x.Selected, x => x.MediaElement, x => x.Category, x => x.Tags).Skip(skip).Take(12).ToList();
                        return new Result<List<Element>>(resultList);
                    }

                    var childCategories = _unitOfWork.ElementCategoryRepository.GetAllIncludingPred(x => x.Parent.Name.ToLower().Trim() == category.ToLower().Trim(), x => x.Parent);

                    var predicate = PredicateBuilder.New<Element>(true);
                    predicate.And(x => String.Equals(x.Category.Name, category, StringComparison.CurrentCultureIgnoreCase));
                    var elementCategories = childCategories as IList<ElementCategory> ?? childCategories.ToList();
                    if (elementCategories.Any())
                    {
                        foreach (var item in elementCategories)
                        {
                            predicate.Or(x => x.Category.Name.ToLower() == item.Name.ToLower());
                        }
                    }

                    var result = _unitOfWork.ElementRepository.GetAllIncludingPred(predicate, x => x.MediaElement, x => x.Category, x => x.Tags).Skip(skip).Take(12).ToList();
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
                return new Result<List<ElementTag>>(_unitOfWork.ElementTagRepository.GetAll().ToList());
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
                var result = _unitOfWork.ElementTagRepository.FindBy(x => x.Name.Trim().ToLower() == text.Trim().ToLower());
                if (result.Any()) return new Result(ResultStatus.ALREADYEXISTS);

                var model = new ElementTag
                {
                    Author = author,
                    Name = text
                };

                _unitOfWork.ElementTagRepository.Add(model);
                _unitOfWork.SaveChanges();
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
                var result = _unitOfWork.ElementTagRepository.GetByIdIncluding(model.Id);
                if (result == null) return new Result(ResultStatus.NOT_FOUND);

                _unitOfWork.ElementTagRepository.Update(model);
                _unitOfWork.SaveChanges();
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
                var result = _unitOfWork.ElementTagRepository.GetById(id);
                if (result == null) return new Result(ResultStatus.NOT_FOUND);

                _unitOfWork.ElementTagRepository.Delete(result);
                _unitOfWork.SaveChanges();
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


                var result = _unitOfWork.ElementRepository.GetAllIncludingPred(predicate, x => x.Category, x => x.MediaElement, x => x.Tags).Skip(skip).Take(12).ToList();
                return new Result<List<Element>>(result);
            }
            catch (Exception ex)
            {
                return new Result<List<Element>>(ResultStatus.ERROR, ex.Message);
            }
        }

        public Result Update(XEditSM model)
        {
            Element elemResult;
            switch (model.Name)
            {
                case "CategoryName":
                    var catResult = _unitOfWork.ElementCategoryRepository.GetById(model.PrimaryKey);
                    if (catResult == null) return new Result(ResultStatus.NOT_FOUND);
                    catResult.Name = model.Value;
                    _unitOfWork.ElementCategoryRepository.Update(catResult);
                    _unitOfWork.SaveChanges();
                    return new Result(ResultStatus.OK);
                case "ElemTitle":
                    elemResult = _unitOfWork.ElementRepository.GetById(model.PrimaryKey);
                    if (elemResult == null) return new Result(ResultStatus.NOT_FOUND);
                    elemResult.Title = model.Value;
                    _unitOfWork.ElementRepository.Update(elemResult);
                    _unitOfWork.SaveChanges();
                    return new Result(ResultStatus.OK);
                case "ElemAuthor":
                    elemResult = _unitOfWork.ElementRepository.GetById(model.PrimaryKey);
                    if (elemResult == null) return new Result(ResultStatus.NOT_FOUND);
                    elemResult.Author = model.Value;
                    _unitOfWork.ElementRepository.Update(elemResult);
                    _unitOfWork.SaveChanges();
                    return new Result(ResultStatus.OK);
                case "ElemDescription":
                    elemResult = _unitOfWork.ElementRepository.GetById(model.PrimaryKey);
                    if (elemResult == null) return new Result(ResultStatus.NOT_FOUND);
                    elemResult.Description = model.Value;
                    _unitOfWork.ElementRepository.Update(elemResult);
                    _unitOfWork.SaveChanges();
                    return new Result(ResultStatus.OK);
                case "ElemVisible":
                    elemResult = _unitOfWork.ElementRepository.GetById(model.PrimaryKey);
                    if (elemResult == null) return new Result(ResultStatus.NOT_FOUND);
                    elemResult.Selected = Convert.ToBoolean(model.Value);
                    _unitOfWork.ElementRepository.Update(elemResult);
                    _unitOfWork.SaveChanges();
                    return new Result(ResultStatus.OK);
                case "ElemCategory":
                    elemResult = _unitOfWork.ElementRepository.GetById(model.PrimaryKey);
                    if (elemResult == null) return new Result(ResultStatus.NOT_FOUND);

                    var elemCategory = _unitOfWork.ElementCategoryRepository.GetById(Convert.ToInt32(model.Value));
                    if (elemCategory == null) return new Result(ResultStatus.NOT_FOUND);

                    elemResult.Category = elemCategory;
                    _unitOfWork.ElementRepository.Update(elemResult);
                    _unitOfWork.SaveChanges();
                    return new Result(ResultStatus.OK);
                case "ElemDate":
                    elemResult = _unitOfWork.ElementRepository.GetById(model.PrimaryKey);
                    if (elemResult == null) return new Result(ResultStatus.NOT_FOUND);

                    elemResult.DatePublished = Convert.ToDateTime(model.Value);
                    _unitOfWork.ElementRepository.Update(elemResult);
                    _unitOfWork.SaveChanges();
                    return new Result(ResultStatus.OK);
                default:
                    return new Result(ResultStatus.NOT_FOUND);
            }
        }

        public Result<Element> UpdateCover(ICollection<IFormFile> files, int id)
        {
            var result = _unitOfWork.ElementRepository.GetByIdIncluding(id, x => x.MediaElement);

            if (result == null) return new Result<Element>(ResultStatus.NOT_FOUND);

            if (files == null || files.Count == 0) return new Result<Element>(ResultStatus.ERROR);

            result.MediaElement = _generalService.CreateMediaElements(files, result.DateCreated.Year, "1", ProjectBaseEnum.Muzeum);

            _unitOfWork.ElementRepository.Update(result);
            _unitOfWork.SaveChanges();
            return new Result<Element>(result);
        }

        public Result<Element> GetNeighbourElementById(int id, bool nextTo)
        {
            try
            {
                var results = _unitOfWork.ElementRepository.GetAllIncluding(x => x.Category, x => x.MediaElement, x => x.Tags).ToList();
                var prev = results.FirstOrDefault();
                var nx = results.LastOrDefault();
                foreach (var item in results)
                {
                    if (item.Id < id && item.Id > prev.Id) { prev = item; }
                    if (item.Id > id && item.Id < nx.Id) { nx = item; }
                }

                if (nextTo && nx != null) return new Result<Element>(nx);
                if (!nextTo && prev != null) return new Result<Element>(prev);
                return new Result<Element>(results.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new Result<Element>(ResultStatus.ERROR, ex.Message);
            }
        }
    }
}
