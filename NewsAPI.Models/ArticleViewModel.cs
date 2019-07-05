using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsAPI.Models
{
    /// <summary>
    /// Article
    /// </summary>
    public class ArticleViewModel
    {

        /// <summary>
        /// Article Id
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Article Headline
        /// </summary>
        /// <value>
        /// The head line.
        /// </value>
        public string HeadLine { get; set; }

        /// <summary>
        /// Article Snippet
        /// </summary>
        /// <value>
        /// The snippet.
        /// </value>
        public string Snippet { get; set; }

        /// <summary>
        /// Article Full Story
        /// </summary>
        /// <value>
        /// The full story.
        /// </value>
        public string FullStory { get; set; }

        /// <summary>
        /// Article Location
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }

        /// <summary>
        /// Determines whether or not the Article is Safe for Work
        /// </summary>
        /// <value>
        ///   <c>true</c> if NSFW; otherwise, <c>false</c>.
        /// </value>
        public bool Nsfw { get; set; }

        /// <summary>
        /// Determines if the Article has a video placeholder
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has video placeholder; otherwise, <c>false</c>.
        /// </value>
        public bool HasVideoPlaceholder { get; set; }

        /// <summary>
        /// Number of images for the Article
        /// </summary>
        /// <value>
        /// The number of images.
        /// </value>
        public int NumberOfImages { get; set; }

        /// <summary>
        /// Category for the Article
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public int CategoryId { get; set; }


        /// <summary>
        /// List of Article Ids related to this Article.
        /// </summary>
        /// <value>
        /// The related article ids.
        /// </value>
        public IEnumerable<int> RelatedArticleIds { get; set; }

    }
}