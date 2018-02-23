using System;
using EKE.Data.Repository.General;
using EKE.Data.Repository.Gyopar;
using EKE.Data.Repository.Muzeum;

namespace EKE.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        //Gyopar
        IArticleRepository ArticleRepository { get; }
        IMagazineCategoryRepository MagazineCategoryRepository { get; }
        IMagazineRepository MagazineRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        ISynonymRepository SynonymRepository { get; }
        IOrderRepository OrderRepository { get; }
        ITagRepository TagRepository { get; }

        //Muzeum
        IElementRepository ElementRepository { get; }
        IElementCategoryRepository ElementCategoryRepository { get; }
        IElementTagsRepository ElementTagRepository { get; }

        //Global
        IMediaElementRepository MediaElementRepository { get; }
        void SaveChanges();
    }
}
