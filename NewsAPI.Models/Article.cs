using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPI.Models
{
    public class Article
    {
        public Article()
        {
            RelatedArticles = new HashSet<RelatedArticles>();
            FeaturedSectionArticles = new HashSet<FeaturedSectionArticles>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string HeadLine { get; set; }

        [Required]
        public string Snippet { get; set; }

        [Required]
        public string FullStory { get; set; }

        [Required]
        public string Location { get; set; }

        public bool Nsfw { get; set; }

        public bool HasVideoPlaceholder { get; set; }

        public int NumberOfImages { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId"), JsonIgnore]
        public virtual Category Category { get; set; }

        public HashSet<RelatedArticles> RelatedArticles { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public HashSet<FeaturedSectionArticles> FeaturedSectionArticles { get; set; }
    }

    public class RelatedArticles
    {
        [Key]
        public int ArticleID { get; set; }

        public Article Article { get; set; }

        [Key]
        public int RelatedArticleID { get; set; }

        public Article RelatedArticle { get; set; }
    }
}
