﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsAPI.Models
{

    public class FeaturedSectionViewModel
    {

        public int Id { get; set; }

        public string Description { get; set; }

        public virtual HashSet<ArticleViewModel> Articles { get; set; }
    }
}
