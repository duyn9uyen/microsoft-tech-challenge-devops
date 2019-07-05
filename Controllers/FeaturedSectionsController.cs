
using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.DataContext;

namespace NewsAPI.Controllers
{

    [Route("api/[controller]")]
    public class FeaturedSectionsController : Controller
    {
        private readonly NewsApiContext _context;

        public FeaturedSectionsController(NewsApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<FeaturedSectionViewModel> GetFeaturedSections()
        {
            var featuredSections = _context.FeaturedSections.Include(a => a.FeaturedSectionArticles).ThenInclude(fa => fa.Article).ThenInclude(a => a.RelatedArticles);
            var featuredSectionsData = from a in featuredSections
                                       select new FeaturedSectionViewModel
                                       {
                                           Id = a.Id,
                                           Description = a.Description,
                                           Articles = GetArticlesFromFeaturedSection(a.FeaturedSectionArticles)
                                       };
            var testvar = featuredSectionsData.FirstOrDefault();
            return featuredSectionsData.AsQueryable();
        }

        private HashSet<ArticleViewModel> GetArticlesFromFeaturedSection(HashSet<FeaturedSectionArticles> featuredSectionArticles)
        {
            var articleList = new HashSet<ArticleViewModel>();

            foreach (var article in featuredSectionArticles)
            {
                var _article = article.Article;
                var articleToAdd = new ArticleViewModel
                {
                    Id = _article.Id,
                    HeadLine = _article.HeadLine,
                    Snippet = _article.Snippet,
                    FullStory = _article.FullStory,
                    Location = _article.Location,
                    Nsfw = _article.Nsfw,
                    HasVideoPlaceholder = _article.HasVideoPlaceholder,
                    NumberOfImages = _article.NumberOfImages,
                    CategoryId = _article.CategoryId,
                    RelatedArticleIds = GetArticleIdList(_article.RelatedArticles)
                };
                articleList.Add(articleToAdd);
            }

            return articleList;
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
    }
}
