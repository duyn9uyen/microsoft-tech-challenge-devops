using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsAPI.Models
{
    /// <summary>
    /// Category
    /// </summary>
    public class CategoryViewModel
    {
        /// <summary>
        /// Category Id.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Category Short Name.
        /// </summary>
        /// <value>
        /// The short name.
        /// </value>
        public string ShortName { get; set; }

        /// <summary>
        /// Category Display Name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }
    }
}