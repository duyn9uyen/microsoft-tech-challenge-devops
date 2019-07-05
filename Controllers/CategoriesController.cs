using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;
using NewsAPI.DataContext;

namespace NewsAPI.Controllers
{

    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly NewsApiContext _context;

        public CategoriesController(NewsApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<CategoryViewModel> GetCategories()
        {
            var categories = _context.Categories.AsQueryable();
            var categoriesResults = from c in categories
                                    select new CategoryViewModel()
                                    {
                                        Id = c.Id,
                                        ShortName = c.ShortName,
                                        DisplayName = c.DisplayName
                                    };

            return categoriesResults;
        }

        [HttpGet("api/Categories/{id}/Articles")]
        public IQueryable<ArticleViewModel> GetArticlesForCategory(int id)
        {
            var articles = _context.Articles.Where(a => a.CategoryId.Equals(id));

            var selectedArticles = articles.Select(a => new ArticleViewModel
            {
                Id = a.Id,
                HeadLine = a.HeadLine,
                Snippet = a.Snippet,
                FullStory = a.FullStory,
                Location = a.Location,
                Nsfw = a.Nsfw,
                HasVideoPlaceholder = a.HasVideoPlaceholder,
                NumberOfImages = a.NumberOfImages,
                CategoryId = a.CategoryId,
                RelatedArticleIds = GetArticleIdList(a.RelatedArticles)
            });
            if (selectedArticles == null)
            {
                throw new Exception();
            }

            return selectedArticles;

        }

        private Comment[] getComments(int aId)
        {
            var Comment = _context.Comments.Where(c => c.ArticleId.Equals(aId)).AsEnumerable().Select(c => new Comment()
            {
                Id = c.Id,
                Name = c.Name,
                EmailAddress = c.EmailAddress,
                CommentText = c.CommentText,
                ArticleId = c.ArticleId,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate
            });
            return Comment.ToArray();
        }

        [HttpGet("{id}")]
        public CategoryViewModel GetCategory(int id)
        {
            var category = _context.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (category == null)
            {
                throw new Exception();
            }

            var categoryResults = new CategoryViewModel()
            {
                Id = category.Id,
                ShortName = category.ShortName,
                DisplayName = category.DisplayName
            };

            return categoryResults;
        }

        private IEnumerable<int> GetArticleIdList(HashSet<RelatedArticles> articleList)
        {
            List<int> idList = new List<int>();

            foreach (var a in articleList)
            {
                idList.Add(a.RelatedArticleID);
            }
            return idList;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
