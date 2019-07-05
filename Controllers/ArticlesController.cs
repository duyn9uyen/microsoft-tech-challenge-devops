using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NewsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DataContext;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly NewsApiContext _context;

        public ArticlesController(NewsApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<ArticleViewModel> GetArticles()
        {
            // return articles
            var articles = from a in _context.Articles
                           select new ArticleViewModel()
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
                           };
            var articlesTest = articles.First();
            return articles.AsQueryable();
        }

        [HttpGet("{id}")]
        public async Task<ArticleViewModel> GetArticle(int id)
        {
            var article = await _context.Articles.Select(a => new ArticleViewModel()
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
            }).FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                throw new Exception();
            }
            return article;
        }

        [HttpGet("api/Articles/{id}/Comments/{pageNumber}/{pageSize}")]
        [ProducesResponseType(typeof(IEnumerable<CommentViewModel>), 200)]
        public async Task<IQueryable<CommentViewModel>> GetCommentsForArticle(int id, int pageNumber = 0, int pageSize = 5)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);
            if (article == null || article.Comments == null)
            {
                throw new Exception();
            }
            var skipCount = pageSize * pageNumber;
            var commentsSkip = article.Comments.Skip(skipCount);
            var commentsTake = commentsSkip.Take(pageSize);
            var comments = commentsTake.ToList();
            var commentsList = from c in comments
                               select new CommentViewModel()
                               {
                                   Id = c.Id,
                                   Name = c.Name,
                                   EmailAddress = c.EmailAddress,
                                   CommentText = c.CommentText,
                                   ArticleId = c.ArticleId,
                                   CreatedDate = c.CreatedDate,
                                   UpdatedDate = c.UpdatedDate
                               };
            return commentsList.AsQueryable();
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
