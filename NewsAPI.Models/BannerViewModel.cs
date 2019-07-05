using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsAPI.Models
{
    /// <summary>
    /// Banner
    /// </summary>
    public class BannerViewModel
    {
        /// <summary>
        /// Banner Id.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Banner Message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
    }
}