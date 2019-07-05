using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsAPI.Models
{

    public class CommentViewModel
    {

        public int Id { get; set; }

        [Required, MaxLength(50, ErrorMessage = "Name may not exceed 50 characters in length")]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string CommentText { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}
