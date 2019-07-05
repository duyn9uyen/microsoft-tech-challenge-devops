using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPI.Models
{
    public class FeaturedSectionArticles
    {
        [Key]
        public int FeaturedSectionID { get; set; }

        public FeaturedSection FeaturedSection { get; set; }

        [Key]
        public int ArticleID { get; set; }

        public Article Article { get; set; }
    }

    public class FeaturedSection
    {
        public FeaturedSection()
        {
            FeaturedSectionArticles = new HashSet<FeaturedSectionArticles>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public HashSet<FeaturedSectionArticles> FeaturedSectionArticles { get; set; }
    }
}

